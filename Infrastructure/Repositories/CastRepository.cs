using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application_Core.Contracts.Repositories;
using Application_Core.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CastRepository : Repository<Cast>, ICastRepository 
    {
        public CastRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Cast> GetById(int id)
        {
            var cast = await _dbContext.Cast.SingleOrDefaultAsync(c => c.Id == id);
            return cast;
        }

        public async Task<List<Cast>> GetCastsByMovie(int movieId)
        {
            var casts = await _dbContext.MovieCast
            .Include(mc => mc.Cast)
            .Where(mc => mc.MovieId == movieId)
            .Select(mc => mc.Cast)
            .OrderBy(c => c.Id)
            .ToListAsync();

            return casts;
        }

        public async Task<List<MovieCast>> GetMovieCastsByMovie(int movieId)
        {
            var movieCasts = await _dbContext.MovieCast
            .Where(mc => mc.MovieId == movieId)
            .OrderBy(mc => mc.CastId)
            .ToListAsync();

            return movieCasts;
        }

        public async Task<List<int>> GetMovieIdsById(int id)
        {
            var allMovieIdsByCast = await _dbContext.MovieCast
            .Where(mc => mc.CastId == id)
            .Select(mc => mc.MovieId)
            .ToListAsync();

            return allMovieIdsByCast;
        }

        public async Task<List<Movie>> GetMoviesById(int id)
        {
            var allMoviesByCast = await _dbContext.MovieCast
            .Include(mc => mc.Movie)
            .Where(mc => mc.CastId == id)
            .Select(mc => mc.Movie)
            .ToListAsync();

            return allMoviesByCast;
        }

    }
}
