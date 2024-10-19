namespace AuctionManagementAPI.Models
{
    public class Auction
{
    public int AuctionId { get; set; }
    public decimal StartingBid { get; set; }
    public decimal CurrentBid { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string Status { get; set; }

    public ICollection<Bid> Bids { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public ICollection<Label> Labels { get; set; }
    public AuctionSchedule AuctionSchedule { get; set; }
}


}
