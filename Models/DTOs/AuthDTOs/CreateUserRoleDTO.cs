using System.ComponentModel.DataAnnotations;

namespace AuctionManagementAPI.Models.DTOs.AuthDTOs
{
    public class CreateUserRoleDTO
    {
        [Required(ErrorMessage = "Role is required.")]
        public required string Role { get; set; }
    }
}
