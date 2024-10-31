using AuctionManagementAPI.Data;
using AuctionManagementAPI.Models;
using AuctionManagementAPI.Models.DTOs.WatchAuctionDTOs;
using Microsoft.EntityFrameworkCore;

namespace AuctionManagementAPI.Services
{
    public class WatchlistService
    {
        private readonly AuctionContext _context;

        public WatchlistService(AuctionContext context)
        {
            _context = context;
        }

        // Add auction to wishlist
        public async Task<string> AddToWatchlistAsync(CreateWatchAuctionDTO createWatchAuctionDTO, int userId)
        {

            // get the user
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == userId);
            if (user == null)
            {
                return "User not found";
            }

            // Check if auction exists
            var auction = await _context.Auctions.FirstOrDefaultAsync(a => a.AuctionId == createWatchAuctionDTO.AuctionId);
            if (auction == null)
            {
                return "Auction not found";
            }

            // create a new watchItem
            var watchlistItem = new WatchAuction
            {
                UserId = userId,
                Auction = auction,
                AuctionId = auction.AuctionId
            };

            await _context.WatchAuctions.AddAsync(watchlistItem);
            await _context.SaveChangesAsync();

            return "Watch List Item created successfully";
        }

        // Get wishlist by user ID
        public async Task<List<WatchAuction>> GetWatchlistByUserIdAsync(int userId)
        {
            return await _context.WatchAuctions
                .Include(w => w.Auction)
                .Where(w => w.UserId == userId)
                .ToListAsync();
        }

        public async Task<string> DeleteWishlistItemAsync(int userId, int watchAuctionId)
        {
            // get the watchAuction item using the userId and watchAuctionId
            var watchAuction = await _context.WatchAuctions.FirstOrDefaultAsync(w => w.UserId == userId && w.WatchAuctionId == watchAuctionId);

            if (watchAuction == null)
                return "Wishlist Item not found";

            // remove the watchAuction item
            _context.WatchAuctions.Remove(watchAuction);
            await _context.SaveChangesAsync();

            return "Wishlist Item was deleted successfully";

        }
    }
}