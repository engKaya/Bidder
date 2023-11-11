using Bidder.BidService.Application.Interfaces.Services;
using Bidder.BidService.Domain.Entities;
using Bidder.Domain.Common.BaseClassess;
using MediatR;

namespace Bidder.BidService.Application.Features.Command.Bidding.GetBid
{
    public class GetBidQueryHandler : IRequestHandler<GetBidQuery, ResponseMessage<Bid>>
    {
        private readonly IBiddingService _bidService;

        public GetBidQueryHandler(IBiddingService bidService)
        {
            _bidService = bidService;
        }

        public Task<ResponseMessage<Bid>> Handle(GetBidQuery request, CancellationToken cancellationToken)
        {

            return _bidService.GetBid(request.BidId, cancellationToken);
        }
    }
}
