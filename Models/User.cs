using static System.Net.WebRequestMethods;

namespace AuctionManagementAPI.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public DateTime LastLogin { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public ActivityLog ActivityLog { get; set; }
        public ICollection<OTP> OTPs { get; set; }
        public UserProfile UserProfile { get; set; }
    }
}
