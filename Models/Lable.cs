using System.ComponentModel.DataAnnotations;

namespace AuctionManagementAPI.Models
{
    public class Lable
    {
        [Key]
        public int LableId { get; set; }
        public required string Name { get; set; }

        public string? Description { get; set; }

        // Navigation properties
        public ICollection<Auction>? Auctions { get; set; }

    }
}
