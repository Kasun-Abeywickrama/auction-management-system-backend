namespace AuctionManagementAPI.Models
{
    public class Label
    {
        public int LabelId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Auction> Auctions { get; set; }
    }

}
