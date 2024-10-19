namespace YourNamespace.Models
{
    public class Auction
    {
        public int AuctionID { get; set; }
        public decimal StartingBid { get; set; }
        public decimal CurrentBid { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Status { get; set; } = string.Empty;
        public ICollection<Bid> Bids { get; set; } = new List<Bid>();
        public User User { get; set; } = new User();
        public Category Category { get; set; } = new Category();
        public ICollection<Label> Labels { get; set; } = new List<Label>();
        public AuctionSchedule AuctionSchedule { get; set; } = new AuctionSchedule();
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }

}
