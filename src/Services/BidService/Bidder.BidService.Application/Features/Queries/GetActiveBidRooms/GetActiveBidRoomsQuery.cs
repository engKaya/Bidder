using Bidder.Domain.Common.BaseClassess;
using Bidder.Domain.Common.Dto.BidService.IBiddingService;
using MediatR;

namespace Bidder.BidService.Application.Features.Queries.GetActiveBidRooms
{
    public class GetActiveBidRoomsQuery : IRequest<ResponseMessage<IEnumerable<ActiveBidRoom>>>
    {
    }
}
