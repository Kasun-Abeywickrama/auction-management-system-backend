namespace YourNamespace.Models
{
    public class Report
    {
        public int ReportID { get; set; }
        public string ReportType { get; set; }
        public string Data { get; set; }
        public DateTime GeneratedOn { get; set; }

        public User Admin { get; set; }
    }
}
