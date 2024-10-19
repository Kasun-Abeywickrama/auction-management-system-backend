namespace AuctionManagementAPI.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ItemTitle { get; set; }
        public string Description { get; set; }
        public int SellerId { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public int ConditionId { get; set; }
        public string[] Images { get; set; }

        public User Seller { get; set; }
        public Category Category { get; set; }
        public Auction Auction { get; set; }
    }

}
