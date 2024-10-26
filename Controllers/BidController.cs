﻿using AuctionManagementAPI.Data;
using AuctionManagementAPI.Models.DTOs.BidDTOs;
using AuctionManagementAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;


namespace AuctionManagementAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class BidController : ControllerBase
    {
        private readonly BidService _bidService;

        public BidController(BidService bidService)
        {
            this._bidService = bidService;
        }

        [HttpGet("GetMyBids")]
        [Authorize]
        public async Task<IActionResult> GetMyBids()
        {
            // get the user id from the token
            var userId = User.FindFirstValue("UserId");

            if (userId == null)
            {
                return BadRequest("User not found");
            }

            // convert the user id to an integer
            int uId = Int32.Parse(userId);

            // Get bids with necessary includes
            var result = await _bidService.GetMyBidsAsync(uId);
            if (result != null)
            {
                var currentTime = DateTime.Now;



                var bids = result
                    .Select(b => new
                    {
                        b.BidId,
                        b.BidAmount,
                        b.TimeStamp,
                        b.AuctionId,
                        ProductName = b.Auction.Product.Name, 
                        ShippingFee = b.Auction.Product.Shippingfee,
                        HighestBidAmount = _bidService.GetHighestBidForAuction(b.AuctionId).Result,
                        TimeStart = b.Auction.StartTime,
                        TimeEnd = b.Auction.EndTime,
                        TimeLeft = $"{(b.Auction.EndTime > currentTime ? (b.Auction.EndTime - currentTime).Days > 0 ? $"{(b.Auction.EndTime - currentTime).Days}d " : "" : "")}" +
                                   $"{(b.Auction.EndTime > currentTime ? (b.Auction.EndTime - currentTime).Hours : 0)}h " +
                                   $"{(b.Auction.EndTime > currentTime ? (b.Auction.EndTime - currentTime).Minutes : 0)} min"
                    })
                    .ToList();

                return Ok(bids);
            }

            return BadRequest("User profile not found");
        }

        //api to delete a bid
        [HttpDelete("DeleteBid/{bidId}")]
        [Authorize]
        public async Task<IActionResult> DeleteBid(int bidId)
        {

            // get the user id from the token
            var userId = User.FindFirstValue("UserId");

            if (userId == null)
            {
                return BadRequest("User not found");
            }

            // convert the user id to an integer
            int uId = Int32.Parse(userId);

            var result = await _bidService.DeleteBidAsync(uId, bidId);
            if (result.Contains("Your bid was deleted successfully"))
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        // api to update a bid
        [HttpPut("UpdateBid/{bidId}")]
        [Authorize]
        public async Task<IActionResult> UpdateBid([FromBody] BidDTO bidDTO)
        {

            // get the user id from the token
            var userId = User.FindFirstValue("UserId");

            if (userId == null)
            {
                return BadRequest("User not found");
            }

            // convert the user id to an integer
            int uId = Int32.Parse(userId);

            var result = await _bidService.UpdateBidAsync(bidDTO, uId);
            if (result.Contains("Bid was updated successfully"))
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpGet("GetBidCount/{auctionId}")]
        [Authorize]
        public async Task<IActionResult> GetBidCount(int auctionId)
        {
            var bidCount = await _bidService.GetBidCountForAuctionAsync(auctionId);
            return Ok(bidCount);
        }

    }
}

