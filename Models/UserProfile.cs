namespace AuctionManagementAPI.Models
{
    public class UserProfile
    {
        public int UserProfileId { get; set; }
        public int UserId { get; set; }
        public double SellerRating { get; set; }
        public string PaymentDetails { get; set; } // Consider using a more structured approach if needed
        public string AdditionalData { get; set; } // JSON field

        public User User { get; set; }
    }

}
