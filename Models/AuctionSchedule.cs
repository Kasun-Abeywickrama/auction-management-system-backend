namespace AuctionManagementAPI.Models
{
    public class AuctionSchedule
    {
        public int AuctionScheduleId { get; set; }
        public int AuctionId { get; set; }
        public DateTime ScheduledStartTime { get; set; }
        public DateTime ScheduledEndTime { get; set; }
        public bool IsRecurring { get; set; }
        public string RecurrencePattern { get; set; } // e.g., "Daily", "Weekly"

        public Auction Auction { get; set; }
    }

}
