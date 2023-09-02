namespace Bidder.BidService.Domain.DTOs.Bidding.CreateBid
{
    public record CreateBidRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal MinPrice { get; set; } = 0;
        public bool HasIncreaseRest { get; set; } = false;
        public decimal MinPriceIncrease { get; set; } = 0;
        public Guid UserId { get; set; }
    }
}
