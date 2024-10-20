namespace AuctionManagementAPI.Models
{
    public class Notification
    {
        public int NotificationId { get; set; }
        public string? NotificationType { get; set; }
        public string? Message { get; set; }
        public DateTime Timestamp { get; set; }

        // Navigation properties
        public User User { get; set; }

        // Foreign key
        public int UserId { get; set; }
    }
}
