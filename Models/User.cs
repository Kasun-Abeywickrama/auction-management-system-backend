namespace YourNamespace.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public DateTime LastLogin { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public ICollection<ActivityLog> ActivityLogs { get; set; }
        public ICollection<OTP> OTPs { get; set; }
        public ICollection<Auction> Auctions { get; set; }
        public ICollection<Bid> Bids { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<Payment> Payments { get; set; }
        public ICollection<Report> Reports { get; set; }
        public UserProfile UserProfile { get; set; }
    }
}
