namespace AuctionManagementAPI.Models
{
    public class Bid
    {
       
        public int BidId { get; set; }
        public decimal BidAmount { get; set; }
        public DateTime TimeStamp { get; set; }
        public bool IsWinningBid {  get; set; }

        // Navigation properties
        public Auction? Auction { get; set; }
        public Payment? Payment { get; set; }
        public required User User { get; set; }
        public ShippingDetails? shippingDetails { get; set; }

        // Foreign key
        public int AuctionId { get; set; }
        public int UserId { get; set; }


    }
}
