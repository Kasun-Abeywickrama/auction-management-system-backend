using System.ComponentModel.DataAnnotations;

namespace AuctionManagementAPI.Models.DTOs.AuctionDTOs
{
    public class CreateAuctionDTO
    {
        // Product properties -------------------------------------------------------------------

        [Required(ErrorMessage = "Name is required.")]
        public required string Name { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Shipping fee is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Shipping fee must be greater than zero.")]
        public required decimal Shippingfee { get; set; }

        [Required(ErrorMessage = "Category ID is required.")]
        public int CategoryId { get; set; }

        // Auction properties -------------------------------------------------------------------

        [Required(ErrorMessage = "Starting bid is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Starting bid must be greater than zero.")]
        public decimal StartingBid { get; set; }

        [Required(ErrorMessage = "Start time is required.")]
        public DateTime StartTime { get; set; }

        [Required(ErrorMessage = "End time is required.")]
        public DateTime EndTime { get; set; }

        public bool? IsSold { get; set; }

        [Required(ErrorMessage = "Shipping method is required.")]
        public required string ShippingMethod { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Package weight must be a non-negative value.")]
        public int? PackageWeight { get; set; }

        public string? PackageDimension { get; set; }
        public bool? IrregularDimension { get; set; }

        [Required(ErrorMessage = "Accept return flag is required.")]
        public bool AcceptReturn { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Return allowed within must be a non-negative value.")]
        public int? ReturnAllowedWithin { get; set; }

        public string? ReturnShippingPaidBy { get; set; }
        public string? ReturnMethod { get; set; }

        // Auction schedule properties -------------------------------------------------------------------

        [Required(ErrorMessage = "Scheduled time is required.")]
        [DataType(DataType.DateTime)]
        public DateTime ScheduledTime { get; set; }

        [Required(ErrorMessage = "Recurring flag is required.")]
        public bool IsRecurring { get; set; }
        public string? RecurrentPattern { get; set; }
    }
}
