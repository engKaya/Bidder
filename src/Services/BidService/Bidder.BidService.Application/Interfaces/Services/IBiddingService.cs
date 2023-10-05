using Bidder.BidService.Application.Features.Command.Bidding.CreateBid;
using Bidder.BidService.Domain.DTOs.Bidding.CreateBid;
using Bidder.BidService.Domain.Entities;
using Bidder.Domain.Common.BaseClassess;
using Bidder.Domain.Common.Dto.BidService.IBiddingService;

namespace Bidder.BidService.Application.Interfaces.Services
{
    public interface IBiddingService
    {
        public Task<ResponseMessage<CreateBidResponse>> CreateBid(Bid request, CancellationToken cancellationToken);
        public Task<ResponseMessage<CreateBidResponse>> CreateBid(CreateBidCommand request, CancellationToken cancellationToken); 
        public Task<ResponseMessage<Bid>> GetBid(Guid BidId, CancellationToken cancellationToken);
        public Task<ResponseMessage<BidRoom>> CreateBidRoom(Bid bid, CancellationToken cancellationToken);
        public Task<ResponseMessage<IEnumerable<ActiveBidRoom>>> GetActiveBidRooms(CancellationToken cancellationToken);
        public Task<ResponseMessage<ActiveBidRoom>> GetActiveBidRoom(string BidId, CancellationToken cancellationToken);


    }
}
