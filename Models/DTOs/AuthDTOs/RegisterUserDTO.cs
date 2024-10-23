using System.ComponentModel.DataAnnotations;

namespace AuctionManagementAPI.Models.DTOs.AuthDTOs
{
    public class RegisterUserDTO
    {
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50)]
        public required string FirstName { get; set; }

        [StringLength(50)]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(50, MinimumLength = 6)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{6,20}$", ErrorMessage = "Password should contain atleast one uppercase, one lowercase, one number and one special character")]
        public required string Password { get; set; }
    }
}
