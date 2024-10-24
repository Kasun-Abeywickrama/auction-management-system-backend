using AuctionManagementAPI.Data;
using AuctionManagementAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace AuctionManagementAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class MyBidsController : ControllerBase
    {
        private readonly AuctionContext auctionContext;

        public MyBidsController(AuctionContext auctionContext)
        {
            this.auctionContext = auctionContext;
        }

        [HttpGet("DisplayMyBids")]
        public IActionResult GetAllMyBids()
        {
            var bids = auctionContext.Bids.ToList();

            if (bids.Count == 0)
            {
                return NotFound("No bids found");
            }
            return Ok(bids);
        }
    }
}
