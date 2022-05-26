using Application_Core.Contracts.Services;
using Application_Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopAPI.Controllers
{
    // Atribute based routing
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private ILogger<MoviesController> _logger;
        public MoviesController(IMovieService movieService, ILogger<MoviesController> logger)
        {
            _movieService = movieService;
            _logger = logger;
        }

        // api/movies/top-grossing
        [Route("top-grossing")]
        [HttpGet]
        public async Task<IActionResult> TopGrossing()
        {
            _logger.LogInformation("Inside Movies Controller");
            var movies = new List<MovieCardModel>
            {
                new MovieCardModel
                {
                    Id = 1, Title = "Test", PosterUrl = "sample poster"
                }
            };

            // ASP.NET Core API will automatically serialize C# objects in to JSON Objects
            // System.Text.Json => 
            // If you are using .NET Core 2 or older or older .NET Framework then JSON serialization are done using a library called
            // Newtonsoft.json
            // 200 OK
            return Ok(movies);
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var movie = await _movieService.GetMovieDetails(id);
            if (movie == null)
            {
                return NotFound(new { ErrorMessage = "No Movie FOund" });
            }
            return Ok(movie);
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetMoviesByPagination([FromQuery] int pageSize = 30, [FromQuery] int page = 1,
            string title = "")
        {
            var movies = await _movieService.GetMoviesByPagination(pageSize, page, title);
            if (movies == null || movies.Count == 0)
            {
                return NotFound("No Movies Was Found");
            }

            return Ok(movies);
        }

        [HttpGet]
        [Route("toprated")]
        public async Task<IActionResult> GetTopRatedMovies()
        {
            var movies = await _movieService.GetTop30RatingMovies();

            if (!movies.Any())
            {
                return NotFound();
            }

            return Ok(movies);
        }

        [HttpGet]
        [Route("toprevenue")]
        public async Task<IActionResult> GetTopRevenueMovies()
        {
            var movies = await _movieService.GetTop30GrossingMovies();

            if (!movies.Any())
            {
                // 404
                return NotFound();
            }

            // 200
            return Ok(movies);
        }

        [HttpGet]
        [Route("genre")]
        public async Task<IActionResult> GetMoviesByGenre(int genreId)
        {
            var movies = await _movieService.GetMoviesOfGenre(genreId);

            if (!movies.Any())
            {
                return NotFound();
            }

            return Ok(movies);
        }

        [HttpGet]
        [Route("{id:int}/reviews")]
        public async Task<IActionResult> GetMovieReviews(int id)
        {
            var reviews = await _movieService.GetReviewsOfMovie(id);

            if (!reviews.Any())
            {
                return NotFound();
            }

            return Ok(reviews);
        }

    }
}

