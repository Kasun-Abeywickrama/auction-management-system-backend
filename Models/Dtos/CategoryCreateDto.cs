namespace AuctionManagementAPI.Dtos
{
    public class CategoryCreateDto
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public int? ParentCategoryId { get; set; }
    }
}