namespace YourNamespace.Models
{
    public class Notification
    {
        public int NotificationID { get; set; }
        public int UserID { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }

        public User User { get; set; }
    }
}
