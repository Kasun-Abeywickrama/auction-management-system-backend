namespace AuctionManagementAPI.Models
{
    public class AddCategoryDto
    {
        public string Name { get; set; } // Ensure that all required fields have correct types
        public string Description { get; set; }
        public int? ParentCategoryId { get; set; } // Nullable if ParentCategory is optional
    }
}