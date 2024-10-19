namespace YourNamespace.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ItemTitle { get; set; }
        public string Description { get; set; }
        public int SellerID { get; set; }
        public int CategoryID { get; set; }
        public int BrandID { get; set; }
        public int ConditionID { get; set; }
        public string[] Images { get; set; }

        public User Seller { get; set; }
        public Category Category { get; set; }
        public ICollection<Auction> Auctions { get; set; }
    }
}
