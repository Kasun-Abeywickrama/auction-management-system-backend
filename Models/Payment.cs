namespace AuctionManagementAPI.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public required int ReferenceNumber { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public required string PaymentMethod { get; set; }

        // Navigation properties
        public Auction? Auction { get; set; }
        public ICollection<Transaction>? Transactions { get; set; }
        public Bid? Bid { get; set; }
        public required User User { get; set; }

        // Foreign key
        public int AuctionId { get; set; }
        public int BidId { get; set; }
        public int UserId { get; set; }

    }
}
