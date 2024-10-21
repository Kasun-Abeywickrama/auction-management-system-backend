namespace AuctionManagementAPI.Models.DTOs
{
    public class VerifyOtpDTO
    {
        public required string Email { get; set; }
        public required string OtpCode { get; set; }
    }
}
