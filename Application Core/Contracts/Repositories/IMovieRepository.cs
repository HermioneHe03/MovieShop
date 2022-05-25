using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application_Core.Entities;
using Application_Core.Models;
using ApplicationCore.Entities;

namespace Application_Core.Contracts.Repositories
{
    public interface IMovieRepository: IRepository<Movie>
    {
        //
        Task<List<Movie>> Get30HighestGrossingMovies();
        Task<List<Movie>> GetTop30RatingMovies();
        Task<List<Cast>> GetCastsByMovie(int movieId);
        Task<List<MovieCast>> GetMovieCastsByMovie(int movieId);
        Task<List<Genre>> GetGenresOfMovie(int movieId);
        Task<List<Movie>> GetMoviesOfGenre(int genreId);
        Task<List<Review>> GetReviewsOfMovie(int movieId);
        Task<List<Trailer>> GetTrailersOfMovie(int movieId);
        Task<PagedResultSet<Movie>> GetMoviesByTitle(int pageSize = 30, int page = 1, string title = "");
        Task<PagedResultSet<Movie>> GetTopPurchasedMovies(int pageSize = 30, int page = 1);
    }
}
