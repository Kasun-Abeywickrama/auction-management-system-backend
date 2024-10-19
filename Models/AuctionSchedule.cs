namespace YourNamespace.Models
{
    public class AuctionSchedule
    {
        public int AuctionScheduleID { get; set; }
        public int AuctionID { get; set; }
        public DateTime ScheduledStartTime { get; set; }
        public DateTime ScheduledEndTime { get; set; }
        public bool IsRecurring { get; set; }
        public string RecurrencePattern { get; set; }

        public Auction Auction { get; set; }
    }
}
