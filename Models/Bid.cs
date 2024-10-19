namespace YourNamespace.Models
{
    public class Bid
    {
        public int BidID { get; set; }
        public int AuctionID { get; set; }
        public int BuyerID { get; set; }
        public decimal BidAmount { get; set; }
        public DateTime Timestamp { get; set; }

        public Auction Auction { get; set; }
        public User Buyer { get; set; }
        public Transaction Transaction { get; set; }
    }
}
