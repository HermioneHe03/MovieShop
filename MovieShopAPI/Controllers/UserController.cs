using Application_Core.Contracts.Services;
using Application_Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("{id:int}/purchases")]
        public async Task<IActionResult> GetAllUserPurchases(int id)
        {
            var purchases = await _userService.GetAllPurchasesForUser(id);
            if (purchases == null)
            {
                return NotFound();
            }

            return Ok(purchases);
        }

        [HttpPost]
        [Route("purchase")]
        public async Task<IActionResult> Purchase([FromBody] PurchaseRequestModel model, int userId)
        {
            var purchase = await _userService.PurchaseMovie(model, userId);

            return Ok(purchase);
        }

        [HttpGet]
        [Route("{id:int}/favorites")]
        public async Task<IActionResult> GetAllUserFavorites(int id)
        {
            var favorites = await _userService.GetAllFavoritesForUser(id);
            if (favorites == null)
            {
                return NotFound();
            }

            return Ok(favorites);
        }

        [HttpGet]
        [Route("{id:int}/movie/{movieId:int}/favorites")]
        public async Task<IActionResult> GetAllUserFavorites(int id, int movieId)
        {
            var favorite = await _userService.FavoriteExists(id, movieId);

            return Ok(favorite);
        }

        [HttpPost]
        [Route("favorite")]
        public async Task AddFavorite([FromBody] FavoriteRequestModel model)
        {
            await _userService.AddFavorite(model);
        }

        [HttpPost]
        [Route("unfavorite")]
        public async Task RemoveFavorite([FromBody] FavoriteRequestModel model)
        {
            await _userService.RemoveFavorite(model);
        }



        [HttpGet]
        [Route("{id:int}/reviews")]
        public async Task<IActionResult> GetAllUserReviews(int id)
        {
            var reviews = await _userService.GetAllReviewsByUser(id);
            if (!reviews.Reviews.Any())
            {
                return NotFound();
            }

            return Ok(reviews.Reviews);
        }

        [HttpPost]
        [Route("review")]
        public async Task AddReview([FromBody] ReviewRequestModel model)
        {
            await _userService.AddMovieReview(model);
        }

    }
}
