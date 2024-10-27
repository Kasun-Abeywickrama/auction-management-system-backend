using System.ComponentModel.DataAnnotations;

namespace AuctionManagementAPI.Models
{
    public class AuctionSchedule
    {
        [Key]
        public int AuctionScheduleId { get; set; }
        public DateTime ScheduledTime { get; set; }
        public bool IsRecurring { get; set; }
        public string? RecurrentPattern { get; set; }

        // Navigation properties
        public Auction? Auction { get; set; }

        // Foreign key
        public int AuctionId { get; set; }

    }
}
