using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuctionManagementAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuctionController : ControllerBase
    {
        // Allow only users with "Admin" role to access this route
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult CreateAuction()
        {
            // Auction creation logic
            return Ok("Auction created");
        }

        // Allow any authenticated user to access this route
        [HttpGet]
        public IActionResult GetAllAuctions()
        {
            // Fetch auctions logic
            return Ok("All auctions");
        }
    }
}
