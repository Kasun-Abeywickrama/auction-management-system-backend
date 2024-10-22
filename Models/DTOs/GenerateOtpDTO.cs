using System.ComponentModel.DataAnnotations;


namespace AuctionManagementAPI.Models.DTOs
{
    public class GenerateOtpDTO
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public required string Email { get; set; }  
    }

}
