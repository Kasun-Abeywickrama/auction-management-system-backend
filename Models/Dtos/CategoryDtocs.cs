namespace AuctionManagementAPI.Dtos
{
    public class CategoryDto
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public List<CategoryDto>? SubCategories { get; set; } = new List<CategoryDto>();
    }
}