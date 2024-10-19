namespace AuctionManagementAPI.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int BuyerId { get; set; }
        public int AuctionId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; }

        public User Buyer { get; set; }
        public Auction Auction { get; set; }
        public Transaction Transaction { get; set; }
    }

}
