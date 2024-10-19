namespace AuctionManagementAPI.Models
{
    public class ActivityLog
    {
        public int ActivityLogId { get; set; }
        public int UserId { get; set; }
        public string ActivityType { get; set; }
        public string ActivityDetails { get; set; } // JSON or structured text
        public DateTime Timestamp { get; set; }

        public User User { get; set; }
    }

}
