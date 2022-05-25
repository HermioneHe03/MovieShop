using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application_Core.Contracts.Repositories;
using Application_Core.Entities;
using Application_Core.Models;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        public MovieRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<List<Movie>> GetTop30GrossingMovies()
        {
            // SQL Database
            // data access logic
            // ADO.NET (Microsoft) SQL Connection, SQLCommand
            // Dapper (ORM) -> StackOverflow
            // Entity Framework Core => LINQ
            // SELECT top 30 * FROM Movie order by Revenue
            // movies.orderbydescnding(m=> m.Revenue).Take(30)
            //they provide both sync and async methods
            var movies = await _dbContext.Movies.OrderByDescending(m => m.Revenue).Take(30).ToListAsync();
            return movies;
        }

        public override async Task<Movie> GetById(int id)
        {
            // we need to join Navigation properties
            // Include method in EF will enable us to join with related navigation properties
            var movie = await _dbContext.Movies.Include(m => m.MoviesOfGenre).ThenInclude(m => m.Genre)
                .Include(m =>m.MovieCasts).ThenInclude(m =>m.Cast)
                .Include(m => m.Trailers)
                .FirstOrDefaultAsync(m => m.Id == id);
            //FirstOrDefault safest one
            //First throws ex when 0 records
            //SingleOrDefault good for 0 or 1
            //Single throw ex when no records found or when more than 1 records found

            return movie;

        }

        public async Task<PagedResultSet<Movie>> MoviesByGenre(int genreId, int pageSize = 30, int pageNumber = 1)
        {
            //get total movies count for that genre
            var totalMoviesCountByGenre = await  _dbContext.MovieGenre.Where(m => m.GenreId == genreId).CountAsync();
            // get the actual movies from MovieGenre and Movie Table
            if (totalMoviesCountByGenre ==0)
            {
                throw new Exception("No Movies Found for that genre");
            }

            var movies = await _dbContext.MovieGenre.Where(g => g.GenreId == genreId).Include(m => m.Movie)
                .OrderBy(m => m.MovieId)
                .Select(m => new Movie
                {
                    Id = m.MovieId, PosterUrl = m.Movie.PosterUrl, Title = m.Movie.Title
                })
                .Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            var pagedMovies = new PagedResultSet<Movie>(movies.ToList(), pageNumber, pageSize, totalMoviesCountByGenre);
            return pagedMovies;
        }
    }
}
