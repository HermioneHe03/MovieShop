using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application_Core.Contracts.Repositories;
using Application_Core.Entities;
using Application_Core.Models;

namespace Infrastructure.Services
{
    public class CastService
    {
        private readonly ICastRepository _castRepository;

        public CastService(ICastRepository castRepository)
        {
            _castRepository = castRepository;
        }

        public async Task<CastDetailsResponseModel> GetCastDetails(int id)
        {
            Cast cast = await _castRepository.GetById(id);
            List<Movie> moviesOfCast = await _castRepository.GetMoviesById(id);
            CastDetailsResponseModel castDetails = CastDetailsResponseModel.FromEntity(cast, moviesOfCast);

            return castDetails;
        }
    }
}
