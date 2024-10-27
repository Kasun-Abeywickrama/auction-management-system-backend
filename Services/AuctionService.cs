using AuctionManagementAPI.Data;
using AuctionManagementAPI.Models;
using AuctionManagementAPI.Models.DTOs.AuctionDTOs;
using Microsoft.EntityFrameworkCore;

namespace AuctionManagementAPI.Services
{
    public class AuctionService
    {
        private readonly AuctionContext _context;

        public AuctionService(AuctionContext context)
        {
            _context = context;
        }

        // Get all auctions without categorys
        public async Task<List<Auction>> GetAuctionsAsync()
        {
            var auctions = await _context.Auctions
                .Include(a => a.Product)
                .ToListAsync();

            return auctions;
        }



        // Get auction by id
        public async Task<Auction> GetAuctionByIdAsync(int auctionId)
        {
            var auction = await _context.Auctions
                .Include(a => a.Product)
                .ThenInclude(p => p.Category)
                .FirstOrDefaultAsync(a => a.AuctionId == auctionId);

            return auction;
        }


        // Get auctions by category withoit categories
        public async Task<List<Auction>> GetAuctionsByCategoryAsync(int categoryId)
        {
            var auctions = await _context.Auctions
                .Include(a => a.Product)
                .Where(a => a.Product.CategoryId == categoryId)
                .ToListAsync();

            return auctions;
        }


        // Create auction
        public async Task<Auction> CreateAuctionAsync(CreateAuctionDTO createAuctionDTO)
        {
            var product = new Product
            {
                Name = createAuctionDTO.Name,
                Description = createAuctionDTO.Description,
                Shippingfee = createAuctionDTO.Shippingfee,
                CategoryId = createAuctionDTO.CategoryId
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            var auction = new Auction
            {
                ProductId = product.ProductId,
                StartingBid = createAuctionDTO.StartingBid,
                StartTime = createAuctionDTO.StartTime,
                EndTime = createAuctionDTO.EndTime,
                IsSold = createAuctionDTO.IsSold,
                ShippingMethod = createAuctionDTO.ShippingMethod,
                PackageWeight = createAuctionDTO.PackageWeight,
                PackageDimension = createAuctionDTO.PackageDimension,
                IrregularDimension = createAuctionDTO.IrregularDimension,
                AcceptReturn = createAuctionDTO.AcceptReturn,
                ReturnAllowedWithin = createAuctionDTO.ReturnAllowedWithin,
                ReturnShippingPaidBy = createAuctionDTO.ReturnShippingPaidBy,
                ReturnMethod = createAuctionDTO.ReturnMethod,
                Product = product
            };

            _context.Auctions.Add(auction);
            await _context.SaveChangesAsync();

            // Create auction schedule
            var auctionSchedule = new AuctionSchedule
            {
                ScheduledTime = createAuctionDTO.ScheduledTime,
                IsRecurring = createAuctionDTO.IsRecurring,
                RecurrentPattern = createAuctionDTO.RecurrentPattern,
                AuctionId = auction.AuctionId
            };

            return auction;
        }
    }
}
