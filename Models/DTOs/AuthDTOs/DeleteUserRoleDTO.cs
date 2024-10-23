using System.ComponentModel.DataAnnotations;

namespace AuctionManagementAPI.Models.DTOs.AuthDTOs
{
    public class DeleteUserRoleDTO
    {
        [Required(ErrorMessage = "UserRoleId is required.")]
        [Range(3, int.MaxValue, ErrorMessage = "UserRoleId must be greater than 2.")] // cannot delete admin role and buyer role
        public int UserRoleId { get; set; }

    }
}
