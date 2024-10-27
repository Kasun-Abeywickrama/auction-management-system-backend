namespace AuctionManagementAPI.Models.DTOs.BidDTOs
{
    public class CreateBidDTO
    {
        public decimal BidAmount { get; set; }

        public DateTime TimeStamp { get; set; }

        public int AuctionId { get; set; }


    }
}
