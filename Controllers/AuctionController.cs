using AuctionManagementAPI.Models.DTOs.AuctionDTOs;
using AuctionManagementAPI.Models.DTOs.CategoryDTOs;
using AuctionManagementAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuctionManagementAPI.Controllers
{
    [Route("api/Auctions")]
    [ApiController]
    public class AuctionController : ControllerBase
    {

        private readonly AuctionService _auctionService;

        public AuctionController(AuctionService auctionService)
        {
            _auctionService = auctionService;
        }

        // GET: api/auctions
        [HttpGet("GetAllAuctions")]
        public async Task<IActionResult> GetAllAuctions()
        {
            var result = await _auctionService.GetAuctionsAsync();

            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("Failed to get auctions");
            }
        }


        // GET: api/auctions/GetAuctionById
        [HttpGet("GetAuctionById")]
        public async Task<IActionResult> GetAuctionById(int auctionId)
        {
            var result = await _auctionService.GetAuctionByIdAsync(auctionId);

            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("Failed to get auction by id");
            }
        }


        // GET: api/auctions/GetAuctionsByCategory
        [HttpGet("GetAuctionsByCategory")]
        public async Task<IActionResult> GetAuctionsByCategory(int categoryId)
        {
            var result = await _auctionService.GetAuctionsByCategoryAsync(categoryId);

            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("Failed to get auctions by category");
            }
        }


        // POST: api/auctions/CreateAuction
        [HttpPost("CreateAuction")]
        public async Task<IActionResult> CreateAuction([FromBody] CreateAuctionDTO createAuctionDTO)
        {
            // check if the model state is valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // create the auction
            var result = await _auctionService.CreateAuctionAsync(createAuctionDTO);

            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("Failed to create auction");
            }

        }







    }
}
