namespace AuctionManagementAPI.Models.DTOs.PaymentDTOs
{
    public class TransactionDTO
    {
        
            public required int ReferenceNumber { get; set; }
            public decimal Amount { get; set; }
            public required string PaymentMethod { get; set; }
            public DateTime PaymentDate { get; set; }
            public bool Status { get; set; }
            
        }

    
}
