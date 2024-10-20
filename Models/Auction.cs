namespace AuctionManagementAPI.Models
{
    public class Auction
    {
        public int AuctionId { get; set; }
        public decimal StartingBid { get; set; }
        public decimal CurrentBid { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string? Status { get; set; }

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
