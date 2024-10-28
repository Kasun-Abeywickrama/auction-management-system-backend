namespace AuctionManagementAPI.Models
{
    public class WatchAuction
    {

        public int WatchAuctionId { get; set; }

        // Navigation properties
        public Auction? Auction { get; set; }
        public Bid? Bid { get; set; }
        public required User User { get; set; }


        // Foreign key
        public int AuctionId { get; set; }
        public int UserId { get; set; }
    }
}
