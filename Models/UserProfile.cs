namespace YourNamespace.Models
{
    public class UserProfile
    {
        public int UserProfileID { get; set; }
        public int UserID { get; set; }
        public double SellerRating { get; set; }
        public string PaymentDetails { get; set; } = string.Empty; // Initialize or make nullable
        public string AdditionalData { get; set; } = string.Empty; // Initialize or make nullable
        public User User { get; set; } = new User(); // Initialize or make nullable
    }

}
