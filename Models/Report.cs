namespace AuctionManagementAPI.Models
{
    public class Report
    {
        public int ReportId { get; set; }
        public string ReportType { get; set; }
        public string Data { get; set; }
        public DateTime GeneratedOn { get; set; }

        public int AdminId { get; set; } // Assuming Admin is a User
        public User Admin { get; set; }
    }

}
