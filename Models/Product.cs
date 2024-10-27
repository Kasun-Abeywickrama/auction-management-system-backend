namespace AuctionManagementAPI.Models
{
    public class Product
    {
        //ProductId, Name, Description, ImageUrls
        public int ProductId { get; set; }
        public required string Name { get; set; }
        public required decimal Shippingfee { get; set; }
        public string? Description { get; set; }
        public List<string>? ImageUrls { get; set; }

        // Navigation properties
        public ICollection<Auction>? Auctions { get; set; }
        public Category? Category { get; set; }

        // Foreign key
        public int CategoryId { get; set; }

    }
}
