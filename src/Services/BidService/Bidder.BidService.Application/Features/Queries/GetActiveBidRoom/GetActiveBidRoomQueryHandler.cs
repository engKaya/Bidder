using Bidder.BidService.Application.Interfaces.Services;
using Bidder.BidService.Domain.Entities;
using Bidder.Domain.Common.BaseClassess;
using MediatR;

namespace Bidder.BidService.Application.Features.Queries.GetActiveBidRoom
{
    public class GetActiveBidRoomQueryHandler : IRequestHandler<GetActiveBidRoomQuery, ResponseMessage<BidRoom>>
    {
        private readonly IBiddingService _bidService;

        public GetActiveBidRoomQueryHandler(IBiddingService bidService)
        {
            _bidService = bidService;
        }

        public async Task<ResponseMessage<BidRoom>> Handle(GetActiveBidRoomQuery request, CancellationToken cancellationToken)
        { 
            return await _bidService.GetActiveBidRoom(request.RoomId, cancellationToken);
        }
    }
}
