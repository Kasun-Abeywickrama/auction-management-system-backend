using System.ComponentModel.DataAnnotations;

namespace AuctionManagementAPI.Models.DTOs.PaymentDTOs
{
    public class ShippingDetailsDTO
    {
        public required string Name { get; set; }
        public required string Address { get; set; }
        public string? ContactNumber { get; set; }
    }
}
