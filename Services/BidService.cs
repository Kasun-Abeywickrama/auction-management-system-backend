using AuctionManagementAPI.Data;
using AuctionManagementAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuctionManagementAPI.Services
{
    public class BidService
    {
        private readonly AuctionContext _context;

        public BidService(AuctionContext context)
        {
            _context = context;
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
        public async Task<decimal> GetHighestBidForAuction(int auctionId)
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
            // check if the bid exists
            var bid = await _context.Bids.FirstOrDefaultAsync(x => x.UserId == userId && x.BidId == bidId);
            if (bid == null)
            {
                return "Bid was not found";
            }

            // remove the bid from the database
            _context.Bids.Remove(bid);
            await _context.SaveChangesAsync();

            return "Your bid was deleted successfully";


        }

    }

    

}


