namespace YourNamespace.Models
{
    public class Transaction
    {
        public int TransactionID { get; set; }
        public int BidID { get; set; }
        public int PaymentID { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }

        public Bid Bid { get; set; }
        public Payment Payment { get; set; }
    }
}
