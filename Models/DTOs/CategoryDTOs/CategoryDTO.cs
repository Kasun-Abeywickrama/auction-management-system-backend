using System.ComponentModel.DataAnnotations;

namespace AuctionManagementAPI.Models.DTOs.CategoryDTOs
{
    public class CategoryDTO
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, ErrorMessage = "Name must be less than 50 characters.")]
        public required string Name { get; set; }

        [StringLength(200, ErrorMessage = "Description must be less than 200 characters.")]
        public string? Description { get; set; }

        public int? ParentCategoryId { get; set; }

    }
}
