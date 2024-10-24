namespace AuctionManagementAPI.Models
{
    public class UpdateCategoryDto
    {
        public string Name { get; set; } // Required properties for updates
        public string Description { get; set; }
        public int? ParentCategoryId { get; set; } // Nullable, as it may not always exist
    }
}