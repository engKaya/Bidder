using Bidder.BidService.Application.Features.Command.Bidding.CreateBid;
using Bidder.BidService.Application.Interfaces.Repos;
using Bidder.BidService.Application.Interfaces.Services;
using Bidder.BidService.Domain.DTOs.Bidding.CreateBid;
using Bidder.BidService.Domain.DTOs.Bidding.GetBidRoom;
using Bidder.BidService.Domain.Entities;
using Bidder.Domain.Common.BaseClassess;
using EventBus.Base.Abstraction;
using Microsoft.Extensions.Logging;

namespace Bidder.BidService.Infastructure.Services
{
    public class BiddingService : IBiddingService
    {
        private readonly IUnitOfWork uof;
        private readonly IEventBus eventBus;
        private readonly IIdentityService identityService;
        private readonly ILogger<BiddingService> logger;

        public BiddingService(IUnitOfWork uof, IEventBus eventBus, IIdentityService identityService, ILogger<BiddingService> logger)
        {
            this.uof = uof;
            this.eventBus = eventBus;
            this.identityService = identityService;
            this.logger = logger;
        }

        public async Task<ResponseMessage<CreateBidResponse>> CreateBid(Bid request, CancellationToken cancellationToken)
        {
            try
            {
                await uof.BidRepository.Add(request, cancellationToken);
                await CreateBidRoom(request, cancellationToken);
                return ResponseMessage<CreateBidResponse>.Success(new CreateBidResponse(request.Id), 200); 
            }
            catch (Exception ex)
            { 
                logger.LogError(ex, ex.StackTrace);
                return ResponseMessage<CreateBidResponse>.Fail("Bid Creation Failed", 500);
            }
        }

        public Task<ResponseMessage<CreateBidResponse>> CreateBid(CreateBidCommand request, CancellationToken cancellationToken)
        {
            Bid bid = new(request.Title, request.Description, request.MinPrice, request.HasIncreaseRest, request.MinPriceIncrease, identityService.GetUserId());
            return CreateBid(bid, cancellationToken);
        }

        public async Task<ResponseMessage<BidRoom>> CreateBidRoom(Bid bid, CancellationToken cancellationToken = default)
        {
            var room = new BidRoom(bid);
            await uof.BidRoomRepository.Add(room, cancellationToken);
            return ResponseMessage<BidRoom>.Success(room, 200);
        }

        public async Task<ResponseMessage<Bid>> GetBid(Guid id)
        {
            var room = await uof.BidRepository.FindFirst(x => x.Id == id, null, x=>x.BidRoom);
            
            if(room == null)
                return ResponseMessage<Bid>.Fail("Bid Room Not Found", 404);
             
            return ResponseMessage<Bid>.Success(room, 200);
        }
    }
}
