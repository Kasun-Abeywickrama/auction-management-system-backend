using System.ComponentModel.DataAnnotations;

namespace AuctionManagementAPI.Models.DTOs.AuthDTOs
{
    public class UpdateUserPermissionDTO
    {

        [Required(ErrorMessage = "UserPermissionId is required.")]
        public int UserPermissionId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string? Description { get; set; }
    }
}
