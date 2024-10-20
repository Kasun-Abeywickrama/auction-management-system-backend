namespace AuctionManagementAPI.Models
{
    public class ActivityLog
    {
        public int ActivityLogId { get; set; }
        public required string ActivityType { get; set; }
        public string? ActivityDetails { get; set; }
        public DateTime Timestamp { get; set; }

        // Navigation properties
        public User User { get; set; }

        // Foreign key
        public int UserId { get; set; }
    }
}
