namespace AuctionManagementAPI.Models
{
    public class UserProfile
    {
        public int UserProfileId { get; set; }
        public decimal SellerRating { get; set; }
        public string? PaymentDetails { get; set; }
        public string? AdditionalData { get; set; }

        // Navigation properties
        public required User User { get; set; }

        // Foreign key
        public int UserId { get; set; }

    }
}
