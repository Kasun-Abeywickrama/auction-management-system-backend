namespace YourNamespace.Models
{
    public class OTP
    {
        public int OTPId { get; set; }
        public int UserID { get; set; }
        public string OTPCode { get; set; }
        public DateTime GeneratedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
        public bool IsUsed { get; set; }
        public string OTPType { get; set; }
        public bool IsValid { get; set; }

        public User User { get; set; }
    }
}
