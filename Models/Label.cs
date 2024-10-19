namespace YourNamespace.Models
{
    public class Label
    {
        public int LabelID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Auction> Auctions { get; set; }
    }
}
