using AuctionManagementAPI.Models;

namespace AuctionManagementAPI.Repositories
{
    public interface IAuctionRepository
    {
        Task<IEnumerable<Auction>> GetAuctionsAsync();
        Task<Auction> GetAuctionByIdAsync(int id);
        Task CreateAuctionAsync(Auction auction);
        Task UpdateAuctionAsync(Auction auction);
    }

}
