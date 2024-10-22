namespace AuctionManagementAPI.Models.DTOs
{
    public class ResetPasswordOtpDTO

    {
        public required string Email { get; set; }
        public required string NewPassword { get; set; }
        public required string ConfirmPassword { get; set; }
        public required string OtpCode { get; set; }
    }
}
