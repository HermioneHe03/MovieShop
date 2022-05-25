using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application_Core.Contracts.Repositories;
using Application_Core.Models;

namespace Application_Core.Contracts.Services
{
    public interface IMovieService
    {
       //home/index action method
        Task<List<MovieCardModel>> GetTop30GrossingMovies();

        Task<MovieDetailsModel> GetMovieDetails(int id);
        Task<PagedResultSet<MovieCardModel>> GetMoviesByGenrePagination(int genreId, int pageSize = 30, int pageNumber =1);
        Task<PagedResultSet<MovieCardModel>> GetTopPurchasedMoviesByPagination(int pageSize, int pageNumber);

    }
}
