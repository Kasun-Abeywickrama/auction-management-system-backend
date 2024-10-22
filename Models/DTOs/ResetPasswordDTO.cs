using System.ComponentModel.DataAnnotations;

namespace AuctionManagementAPI.Models.DTOs
{
    public class ResetPasswordOtpDTO

    {
        [Required (ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(50, MinimumLength = 6)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{6,20}$", ErrorMessage = "Password should contain atleast one uppercase, one lowercase, one number and one special character")]
        public required string NewPassword { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(50, MinimumLength = 6)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{6,20}$", ErrorMessage = "Password should contain atleast one uppercase, one lowercase, one number and one special character")]
        public required string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "OTP is required.")]
        [StringLength(6, MinimumLength = 6)]
        public required string OtpCode { get; set; }
    }
}
