namespace AuctionManagementAPI.Models
{
    public class User
    {
        public int UserId { get; set; }
        public required string FirstName { get; set; }
        public string? LastName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string PasswordSalt { get; set; }
        public DateTime? LastLogin { get; set; }
        public bool IsVerified { get; set; } = false;  
        public int FailedLoginAttempts { get; set; } = 0;

        // Navigation properties
        public ICollection<Payment>? Payments { get; set; }
        public ICollection<Bid>? Bids { get; set; }
        public ICollection<Report>? Reports { get; set; }
        public UserRole? UserRole { get; set; }
        public UserProfile? UserProfile { get; set; }
        public ICollection<Notification>? Notifications { get; set; }
        public ICollection<ActivityLog>? ActivityLogs { get; set; }
        public ICollection<Otp>? Otps { get; set; }

        // new
        public ICollection<ShippingDetails>? ShippingDetails { get; set; }

        // Foreign key
        public int UserRoleId { get; set; }
    }
}
