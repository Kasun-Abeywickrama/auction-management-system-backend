namespace AuctionManagementAPI.Models
{
    public class Product
    {
        //ProductId, Name, Description, Images
        public int ProductId { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public List<string>? Images { get; set; }

        // Navigation properties
        public ICollection<Auction>? Auctions { get; set; }
        public Category? Category { get; set; }

        // Foreign key
        public int CategoryId { get; set; }

    }
}
