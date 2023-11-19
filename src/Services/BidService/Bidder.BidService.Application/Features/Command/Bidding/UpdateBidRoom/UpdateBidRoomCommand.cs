using Bidder.BidService.Domain.Entities;
using Bidder.Domain.Common.BaseClassess;
using MediatR;

namespace Bidder.BidService.Application.Features.Command.Bidding.UpdateBidRoom
{
    public class UpdateBidRoomCommand : IRequest<ResponseMessage<BidRoom>>
    {
        public BidRoom BidRoom { get; set; }
        public UpdateBidRoomCommand(BidRoom bidRoom)
        {
            BidRoom = bidRoom;
        }
    }
}
