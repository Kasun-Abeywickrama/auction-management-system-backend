namespace AuctionManagementAPI.Models
{
    public class ShippingDetails
    {
        public int ShippingDetailsId { get; set; }
        public required string Name { get; set; }
        public required string Address { get; set; }
        public string? ContactNumber { get; set; }

        // Navigation Properties
        public User? User { get; set; }

        public required Bid Bid { get; set; }


        // Foreign Key for User
        public int UserId { get; set; }
        
        public int BidId { get; set; }

       
    }
}
