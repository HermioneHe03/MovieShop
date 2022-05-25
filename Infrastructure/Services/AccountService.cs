using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Application_Core.Contracts.Repositories;
using Application_Core.Contracts.Services;
using Application_Core.Entities;
using Application_Core.Exceptions;
using Application_Core.Models;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;
        private AccountService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<UserLoginResponseModel> LoginUser(string email, string password)
        {
            // go to database and get the user row by email
            var dbUser = await _userRepository.GetUserByEmail(email);
            if (dbUser == null)
            {
                throw new Exception("Email does not exist");
            }

            var hashedPassord = GetHashedPassword(password, dbUser.Salt);

            if (hashedPassord == dbUser.HashedPassword)
            {
                //password is good
                var userLoginResponseModel = new UserLoginResponseModel
                {
                    Id = dbUser.Id,
                    Email = dbUser.Email,
                    FirstName = dbUser.FirstName,
                    LastName = dbUser.LastName
                };
                return userLoginResponseModel;
            }
            return null;
        }

        public async Task<bool> RegisterUser(UserRegisterModel model)
        {
            // go to database and CHECK IF THE EMAIL EXISTS ALREADY
            var dbUser = await _userRepository.GetUserByEmail(model.Email);
            if (dbUser != null)
            {
                //user already exists
                throw new ConflictException("Email already exists");
            }
            //create a random salt
            var salt = GetRandomSalt();

            //create hashed password with the salt created above
            // Never ever create your own hashing algorithms
            //Pdbkf2 (Microsoft), Bcrypt, Aargon2
            var hashedPassword = GetHashedPassword(model.Password, salt);
            
            var user = new User
            {
                Email = model.Email,
                Salt = salt,
                HashedPassword = hashedPassword,
                FirstName = model.FirstName,
                LastName = model.LastName,
                DateOfBirth = model.DateOfBirth
            };

            var createdUser = await _userRepository.Add(user);

            if (createdUser.Id>0)
            {
                return true;
            }
            return false;

            //save the user opject to User Table
        }

        
        private string GetRandomSalt()
        {
            var randomBytes = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }
            return Convert.ToBase64String(randomBytes);

        }

        private string GetHashedPassword(string password, string salt)
        {
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
           password,
           Convert.FromBase64String(salt),
           KeyDerivationPrf.HMACSHA512,
           10000,
           256 / 8));
            return hashed;
        }
    }
}
