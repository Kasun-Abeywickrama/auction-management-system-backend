using AuctionManagementAPI.Models;
using AuctionManagementAPI.Repositories;

namespace AuctionManagementAPI.Services
{
    public class AuctionService
    {
        private readonly IAuctionRepository _auctionRepository;

        public AuctionService(IAuctionRepository auctionRepository)
        {
            _auctionRepository = auctionRepository;
        }

        public async Task<IEnumerable<Auction>> GetAuctionsAsync()
        {
            return await _auctionRepository.GetAuctionsAsync();
        }

        public async Task<Auction> GetAuctionByIdAsync(int id)
        {
            return await _auctionRepository.GetAuctionByIdAsync(id);
        }

        // Add the missing CreateAuctionAsync method
        public async Task CreateAuctionAsync(Auction auction)
        {
            await _auctionRepository.CreateAuctionAsync(auction);
        }
    }

}
