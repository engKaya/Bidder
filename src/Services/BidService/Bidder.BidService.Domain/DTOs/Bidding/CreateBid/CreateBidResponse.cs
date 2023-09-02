namespace Bidder.BidService.Domain.DTOs.Bidding.CreateBid
{
    public class CreateBidResponse
    {
        public CreateBidResponse(Guid bidId)
        {
            BidId = bidId;
        }

        public Guid BidId { get; set; }
    }
}
