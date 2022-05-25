using Application_Core.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;
        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }
        public async Task<IActionResult> Details(int id)
        {
            // go to movies table and get the movie details by ID
            // connect to SQL server and execute the SQL query
            //select * from Movie where id=2
            // get the movies entity (object)
            // Repositories => Data access Logic
            //Services => Business Logic
            // Controllers action methods => Services methods => Repository methods => SQL database
            // get the mode data from the sevices and send the data to the views(M)
            // Onion architecture or N-Layer architecture
            //Remote Database
            //CPU bound operation => PI => Loan calculator, image pro
            //I/O bound operation =>database calls, file, images, videos

            // Network speed, SQL Server => Query, Server Memory
            // T1 is just waiting
            var movieDetails = await _movieService.GetMovieDetails(id);
            return View(movieDetails);
        }

        [HttpGet]
        public async Task<IActionResult> Genre(int id)
        {
            int genreId = id;
            var movieModels = await _movieService.GetMoviesOfGenre(genreId);

            return View(movieModels);
        }
    }
}
