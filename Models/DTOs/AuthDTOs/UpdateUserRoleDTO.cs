using System.ComponentModel.DataAnnotations;

namespace AuctionManagementAPI.Models.DTOs.AuthDTOs
{
    public class UpdateUserRoleDTO
    {

        [Required(ErrorMessage = "UserRoleId is required.")]
        [Range(3, int.MaxValue, ErrorMessage = "UserRoleId must be greater than 2.")] // cannot update admin role and buyer role
        public int UserRoleId { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        public required string Role { get; set; }
    }
}
