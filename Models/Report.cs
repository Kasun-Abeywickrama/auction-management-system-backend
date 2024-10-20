namespace AuctionManagementAPI.Models
{
    public class Report
    {
        public int ReportId { get; set; }
        public required string ReportType { get; set; }
        public string? ReportName { get; set; }
        public string? Data { get; set; }
        public DateTime GeneratedOn { get; set; }
        public required User User { get; set; }

        // Foreign key
        public int UserId { get; set; }
    }
}
