using AuctionManagementAPI.Models;
using AuctionManagementAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuctionManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuctionsController : ControllerBase
    {
        private readonly AuctionService _auctionService;

        public AuctionsController(AuctionService auctionService)
        {
            _auctionService = auctionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAuctions()
        {
            var auctions = await _auctionService.GetAuctionsAsync();
            return Ok(auctions);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAuction(Auction auction)
        {
            await _auctionService.CreateAuctionAsync(auction);
            return CreatedAtAction(nameof(GetAuctions), new { id = auction.AuctionId }, auction);
        }
    }

}
