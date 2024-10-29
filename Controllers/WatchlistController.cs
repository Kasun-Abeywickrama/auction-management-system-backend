using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AuctionManagementAPI.Models;
using AuctionManagementAPI.Services;
using AuctionManagementAPI.Models.DTOs.WatchAuctionDTOs;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;


namespace AuctionManagementAPI.Controllers
{

    [Route("api/watchlist")]
    [ApiController]
    public class WatchlistController : ControllerBase
    {
        private readonly WatchlistService _watchlistService;

        public WatchlistController(WatchlistService watchlistService)
        {
            this._watchlistService = watchlistService;
        }

      
        [HttpPost("AddWatchAuction/{auctionId}")]
        [Authorize]
        public async Task<IActionResult> AddToWatchlist([FromBody] CreateWatchAuctionDTO createWatchAuctionDTO)
        {
            var userId = User.FindFirstValue("UserId");

            if (userId == null)
            {
                return BadRequest("User not found");
            }

            // convert the user id to an integer
            int uId = Int32.Parse(userId);

            var result = await _watchlistService.AddToWatchlistAsync(createWatchAuctionDTO, uId);
            if (result.Contains("Watch List Item created successfully"))
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

       
        [HttpGet("MyWatchlist")]
        public async Task<IActionResult> GetWatchlistByUserIdAsync()
        {
            // get the user id from the token
            var userId = User.FindFirstValue("UserId");

            if (userId == null)
            {
                return BadRequest("User not found");
            }

            // convert the user id to an integer
            int uId = Int32.Parse(userId);
            var watchlist = await _watchlistService.GetWatchlistByUserIdAsync(uId);

            if (watchlist != null)
            {
                return Ok(watchlist);
            }
            else
            {
                return NotFound("Watchlist not found for this user.");
            }
        }

        [HttpDelete("DeleteWatchlistItem/{watchAuctionId}")]
        [Authorize]
        public async Task<IActionResult> DeleteBid(int watchAuctionId)
        {

            // get the user id from the token
            var userId = User.FindFirstValue("UserId");

            if (userId == null)
            {
                return BadRequest("User not found");
            }

            // convert the user id to an integer
            int uId = Int32.Parse(userId);

            var result = await _watchlistService.DeleteWatchlistItemAsync(uId, watchAuctionId);
            if (result.Contains("Watchlist Item was deleted successfully"))
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
