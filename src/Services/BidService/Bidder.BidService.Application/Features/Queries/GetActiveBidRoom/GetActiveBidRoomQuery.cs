using Bidder.BidService.Domain.Entities;
using Bidder.Domain.Common.BaseClassess;
using MediatR;

namespace Bidder.BidService.Application.Features.Queries.GetActiveBidRoom
{
    public class GetActiveBidRoomQuery : IRequest<ResponseMessage<BidRoom>>
    {
        public long RoomId { get; set; }
        public GetActiveBidRoomQuery(long roomId)
        {
            RoomId = roomId;
        }  
    }
}
