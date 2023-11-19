using Bidder.BidService.Application.Interfaces.Services;
using Bidder.Domain.Common.BaseClassess;
using Bidder.Domain.Common.Dto.BidService.IBiddingService;
using MediatR;

namespace Bidder.BidService.Application.Features.Queries.GetActiveBidRooms
{
    public class GetActiveBidRoomsQueryHandler : IRequestHandler<GetActiveBidRoomsQuery, ResponseMessage<IEnumerable<ActiveBidRoom>>>
    {
        private readonly IBiddingService _bidService;

        public GetActiveBidRoomsQueryHandler(IBiddingService bidService)
        {
            _bidService = bidService;
        }

        public Task<ResponseMessage<IEnumerable<ActiveBidRoom>>> Handle(GetActiveBidRoomsQuery request, CancellationToken cancellationToken)
        {
            return _bidService.GetActiveBidRooms(cancellationToken);
        }
    }
}
