using Bidder.BidService.Domain.Entities;
using Bidder.Domain.Common.BaseClassess;
using MediatR;

namespace Bidder.BidService.Application.Features.Command.Bidding.GetBid
{
    public record GetBidQuery : IRequest<ResponseMessage<Bid>>
    {
        public Guid BidId;

        public GetBidQuery(Guid bidId)
        {
            BidId = bidId;
        }
    }
}
