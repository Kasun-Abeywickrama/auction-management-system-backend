namespace AuctionManagementAPI.Models
{
    public class Bid
    {
        public int BidId { get; set; }
        public int AuctionId { get; set; }
        public int BuyerId { get; set; }
        public decimal BidAmount { get; set; }
        public DateTime Timestamp { get; set; }

        public Auction Auction { get; set; }
        public User Buyer { get; set; }
    }

}
