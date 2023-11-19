using Bidder.BidService.Application.Interfaces.Services;
using Bidder.BidService.Domain.Entities;
using Bidder.Domain.Common.BaseClassess;
using MediatR;

namespace Bidder.BidService.Application.Features.Command.Bidding.UpdateBidRoom
{
    public class UpdateBidRoomCommandHandler : IRequestHandler<UpdateBidRoomCommand, ResponseMessage<BidRoom>>
    {
        private readonly IBiddingService _bidService;

        public UpdateBidRoomCommandHandler(IBiddingService bidService)
        {
            _bidService = bidService;
        } 
        public async Task<ResponseMessage<BidRoom>> Handle(UpdateBidRoomCommand request, CancellationToken cancellationToken)
        {
            var response = await _bidService.UpdateBidRoom(request.BidRoom);
            return response;
        }
    }
}
