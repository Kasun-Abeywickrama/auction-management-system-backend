namespace AuctionManagementAPI.Models
{
    public class Auction
    {
        public int AuctionId { get; set; }
        public decimal StartingBid { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool? IsSold { get; set; }
        public DateTime? ScheduledTime { get; set; }
        public required string ShippingMethod { get; set; }
        public int? PackageWeight { get; set; }
        public string? PackageDimension { get; set; }
        public bool? IrregularDimension { get; set; }
        public bool AcceptReturn { get; set; }
        public int? ReturnAllowedWithin { get; set; }
        public string? ReturnShippingPaidBy { get; set; }
        public string? ReturnMethod { get; set; }


        // Navigation properties
        public ICollection<Bid>? Bids { get; set; }
        public AuctionSchedule? AuctionSchedule { get; set; }
        public ICollection<Lable>? Lables { get; set; }

        public required Product Product { get; set; }
        public Payment? Payment { get; set; }

        // Foreign key
        public int ProductId { get; set; }
    }

}
