namespace AuctionManagementAPI.Models
{
    public class Auction
    {
        public int AuctionId { get; set; }
        public string? Title { get; set; }  // Nullable string
        public decimal StartingPrice { get; set; }
        public decimal CurrentPrice { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public List<Bid>? Bids { get; set; }  // Nullable list
        public int UserId { get; set; }
    }

}
