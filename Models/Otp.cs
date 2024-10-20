namespace AuctionManagementAPI.Models
{
    public class Otp
    {
        public int OtpId { get; set; }
        public required string OtpType { get; set; }
        public required string OtpCode { get; set; }
        public DateTime GeneratedAt { get; set; } 
        public DateTime ExpiresAt { get; set; }
        public bool IsUsed { get; set; }

        // Navigation properties
        public required User User { get; set; }

        // Foreign key
        public int UserId { get; set; }
    }
}
