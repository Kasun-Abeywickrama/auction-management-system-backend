using AuctionManagementAPI.Data;
using AuctionManagementAPI.Models;
using Microsoft.EntityFrameworkCore;


namespace AuctionManagementAPI.Repositories
{
    public class AuctionRepository : IAuctionRepository
    {
        private readonly ApplicationDbContext _context;

        public AuctionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Auction>> GetAuctionsAsync()
        {
            return await _context.Auctions.Include(a => a.Bids).ToListAsync();
        }

        public async Task<Auction> GetAuctionByIdAsync(int id)
        {
            var auction = await _context.Auctions.Include(a => a.Bids)
                                                 .FirstOrDefaultAsync(a => a.AuctionId == id);
            if (auction == null)
            {
                throw new Exception("Auction not found");
            }
            return auction;
        }


        public async Task CreateAuctionAsync(Auction auction)
        {
            _context.Auctions.Add(auction);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAuctionAsync(Auction auction)
        {
            _context.Auctions.Update(auction);
            await _context.SaveChangesAsync();
        }
    }

}
