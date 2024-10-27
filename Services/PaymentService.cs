using AuctionManagementAPI.Data;
using AuctionManagementAPI.Migrations;
using AuctionManagementAPI.Models;
using AuctionManagementAPI.Models.DTOs.PaymentDTOs;
using Microsoft.EntityFrameworkCore;

namespace AuctionManagementAPI.Services
{
    public class PaymentService
    {
        public readonly AuctionContext _context;

        public PaymentService(AuctionContext context)
        { 
           _context = context;
        }

        public async Task<string> AddShippingDetailsAsync (ShippingDetailsDTO shippingDetailsDTO, int userId)
        {

            // check if shipping details already exists
            var ShippingDetails = await _context.ShippingDetails.FirstOrDefaultAsync(x => x.UserId == userId);
            if (ShippingDetails != null)
            {
                return "User shipping details already exists";
            }

            // get shipping details
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == userId);
            if (user == null)
            {
                return "User not found";
            }

            //get the winning bid of this user.
            var lastWinningBid = await _context.Bids
                .Where(b => b.UserId == userId && b.IsWinningBid)
                .OrderByDescending(b => b.TimeStamp)
                .FirstOrDefaultAsync();

            if (lastWinningBid == null)
            {
                return "shipping details not found";
            }

            // Add shipping details
            var newShippingDetails = new ShippingDetails
            {
                Name = shippingDetailsDTO.Name,
                Address = shippingDetailsDTO.Address,
                ContactNumber = shippingDetailsDTO.ContactNumber,
                User = user,
                Bid = lastWinningBid,
                UserId = userId

            };

            // add shipping Details to the database
            await _context.ShippingDetails.AddAsync (newShippingDetails);
            await _context.SaveChangesAsync();

            return "Shipping Details Added successfully!";

        }


        public async Task<string> UpdateShippingDetailsAsync(ShippingDetailsDTO shippingDetailsDTO, int userId)
        {
            // check if Shipping Details exists
            var ShippingDetails = await _context.ShippingDetails.FirstOrDefaultAsync(x => x.UserId == userId);
            if (ShippingDetails == null)
            {
                return "User profile not found";
            }

            // update Shipping Details
            ShippingDetails.Name = shippingDetailsDTO.Name;
            ShippingDetails.Address = shippingDetailsDTO.Address;
            ShippingDetails.ContactNumber = shippingDetailsDTO.ContactNumber;

            // save the changes to the database
            await _context.SaveChangesAsync();

            return "Shipping Details updated successfully";
        }

        public async Task<ShippingDetails?> GetShippingDetailsAsync(int userId)
        {

            // get Shipping Details
            var shippingDetails = await _context.ShippingDetails.FirstOrDefaultAsync(x => x.UserId == userId);

            if (shippingDetails == null)
            {
                return null;
            }

            return shippingDetails;
        }
    }
}
