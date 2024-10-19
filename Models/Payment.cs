namespace YourNamespace.Models
{
    public class Payment
    {
        public int PaymentID { get; set; }
        public int BuyerID { get; set; }
        public int AuctionID { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; }

        public User Buyer { get; set; }
        public Auction Auction { get; set; }
        public Transaction Transaction { get; set; }
    }
}
