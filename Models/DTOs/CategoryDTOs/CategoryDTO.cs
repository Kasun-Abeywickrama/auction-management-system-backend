namespace AuctionManagementAPI.Models.DTOs.CategoryDTOs
{
    public class CategoryDTO
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public int? ParentCategoryId { get; set; }

    }
}
