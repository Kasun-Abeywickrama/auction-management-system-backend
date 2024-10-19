namespace AuctionManagementAPI.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int BidId { get; set; }
        public int PaymentId { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }

        public Bid Bid { get; set; }
        public Payment Payment { get; set; }
    }

}
