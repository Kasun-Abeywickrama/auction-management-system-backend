namespace YourNamespace.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ParentCategoryID { get; set; }

        public Category ParentCategory { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<Auction> Auctions { get; set; }
        public ICollection<Category> SubCategories { get; set; }
    }
}
