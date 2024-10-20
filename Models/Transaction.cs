using System.ComponentModel.DataAnnotations;

namespace AuctionManagementAPI.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }
        public bool Status { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }

        // Navigation properties
        public required Payment Payment { get; set; }

        // Foreign keys
        public int PaymentId { get; set; }
    }
}
