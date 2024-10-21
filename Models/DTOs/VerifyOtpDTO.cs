namespace AuctionManagementAPI.Models.DTOs
{
    public class VerifyOtpDTO
    {
        public int UserId { get; set; }
        public required string OtpCode { get; set; }
    }
}
