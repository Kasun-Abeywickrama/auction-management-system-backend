namespace YourNamespace.Models
{
    public class ActivityLog
    {
        public int ActivityLogID { get; set; }
        public int UserID { get; set; }
        public string ActivityType { get; set; }
        public string ActivityDetails { get; set; }
        public DateTime Timestamp { get; set; }

        public User User { get; set; }
    }
}
