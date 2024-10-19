namespace AuctionManagementAPI.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ParentCategoryId { get; set; }

        public ICollection<Product> Products { get; set; }
        public Category ParentCategory { get; set; } // Self-referencing
    }

}
