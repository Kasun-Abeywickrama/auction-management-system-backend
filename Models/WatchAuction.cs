namespace AuctionManagementAPI.Models
{
    public class WatchAuction
    {

        public int WatchAuctionId { get; set; }

        // Navigation properties
        public Bid? Bid { get; set; }
        public ICollection<User>? Users { get; set; }
        public Auction? Auction { get; set; }


        // Foreign key
        public required int AuctionId { get; set; }
        public required int UserId { get; set; }
    }
}
