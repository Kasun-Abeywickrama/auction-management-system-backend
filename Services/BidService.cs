using AuctionManagementAPI.Data;
using AuctionManagementAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AuctionManagementAPI.Models.DTOs.BidDTOs;
using AuctionManagementAPI.Models.DTOs.UserProfileDTOs;
using Microsoft.AspNetCore.Http.HttpResults;

namespace AuctionManagementAPI.Services
{
    public class BidService
    {
        private readonly AuctionContext _context;

        public BidService(AuctionContext context)
        {
            _context = context;
        }



        public async Task<string> CreateBidAsync(CreateBidDTO createBidDTO, int userId)
        {

            // get the user
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == userId);
            if (user == null)
            {
                return "User not found";
            }

            // Check if auction exists
            var auction = await _context.Auctions.FirstOrDefaultAsync(a => a.AuctionId == createBidDTO.AuctionId);
            if (auction == null)
            {
                return "Auction not found";
            }
            // create a new bid
            var newBid = new Bid
            {
                BidAmount = createBidDTO.BidAmount,
                User = user,
                UserId = userId,
                Auction = auction,      // Associate with the existing auction
                AuctionId = auction.AuctionId

            };

            // add the new bid to the database
            await _context.Bids.AddAsync(newBid);
            await _context.SaveChangesAsync();

            return "Bid was created successfully";

        }


        // get my bids
        public async Task<List<Bid>> GetMyBidsAsync(int userId)
            {
                // get all bids for the given userId and include related entities
                var myBids = await _context.Bids
                    .Where(x => x.UserId == userId)
                    .Include(b => b.Auction)
                    .ThenInclude(a => a.Product)
                    .ToListAsync();

                return myBids;

                
            }
        public async Task<decimal> GetHighestBidForAuctionAsync(int auctionId)
        {
            var highestBid = await _context.Bids
                .Where(b => b.AuctionId == auctionId)
                .OrderByDescending(b => b.BidAmount)
                .Select(b => b.BidAmount)
                .FirstOrDefaultAsync();

            return highestBid;
        }



        public async Task<List<Bid>> GetAllBidsAsync()
        {
            // get all bids
            var bids = await _context.Bids.ToListAsync();

            return bids;
        }

        public async Task<string> DeleteBidAsync(int userId, int bidId)
        {
            // get the bid using the bidId and userId
            var bid = await _context.Bids.FirstOrDefaultAsync(x => x.UserId == userId && x.BidId == bidId);
            if (bid == null)
                return "Bid was not found or does not belong to the user";

            // remove the bid from the database
            _context.Bids.Remove(bid);
            await _context.SaveChangesAsync();


            return "Your bid was deleted successfully";


        }


        public async Task<string> UpdateBidAsync(BidDTO bidDTO, int userId)
        {
            // Check if the bid exists and is owned by the user
            var bid = await _context.Bids.FirstOrDefaultAsync(x => x.UserId == userId && x.BidId == bidDTO.BidId);
            if (bid == null)
            {
                return "Bid was not found or does not belong to the user";
            }

            // Update the bid amount
            bid.BidAmount = bidDTO.BidAmount;

            // Save changes to the database
            await _context.SaveChangesAsync();

            return "Bid was updated successfully";
        }



        public async Task<int> GetBidCountForAuctionAsync(int auctionId)
        {
            return await _context.Bids.CountAsync(b => b.AuctionId == auctionId);
        }


    }



}


