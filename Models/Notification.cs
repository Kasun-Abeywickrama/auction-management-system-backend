namespace AuctionManagementAPI.Models
{
    public class Notification
    {
        public int NotificationId { get; set; }
        public string NotificationType { get; set; }
        public string Title { get; set; }
        public string? Message { get; set; }
        public bool IsRead { get; set; }
        public DateTime? ScheduledTime { get; set; }
        public DateTime Timestamp { get; set; }
        public DateTime? ReadAt { get; set; }

        // Navigation properties
        public User? User { get; set; }

        // Foreign key
        public required int UserId { get; set; }
    }
}
