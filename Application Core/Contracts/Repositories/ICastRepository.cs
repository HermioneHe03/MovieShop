using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application_Core.Entities;

namespace Application_Core.Contracts.Repositories
{
    public interface ICastRepository: IRepository<Cast>
    {
        Task<List<int>> GetMovieIdsById(int id);
        Task<List<Movie>> GetMoviesById(int id);
        Task<List<Cast>> GetCastsByMovie(int movieId);
        Task<List<MovieCast>> GetMovieCastsByMovie(int movieId);
    }
}
