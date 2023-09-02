using Bidder.BidService.Domain.DTOs.Bidding.CreateBid;
using Bidder.Domain.Common.BaseClassess;
using MediatR;

namespace Bidder.BidService.Application.Features.Command.Bidding.CreateBid
{
    public class CreateBidCommand : IRequest<ResponseMessage<CreateBidResponse>>
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal MinPrice { get; set; } = 0;
        public bool HasIncreaseRest { get; set; } = false;
        public decimal MinPriceIncrease { get; set; } = 0; 
    }
}
