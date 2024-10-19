namespace AuctionManagementAPI.Models
{
    public class OTP
    {
        public int OTPId { get; set; }
        public int UserId { get; set; }
        public string OTPCode { get; set; }
        public DateTime GeneratedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
        public bool IsUsed { get; set; }
        public string OTPType { get; set; } // e.g., "Login", "Payment"
        public bool IsValid { get; set; }

        public User User { get; set; }
    }

}
