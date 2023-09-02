using Bidder.BidService.Application.Interfaces.Repos;
using Bidder.BidService.Domain.DTOs.Bidding.CreateBid;
using Bidder.BidService.Domain.Entities;
using Bidder.Domain.Common.BaseClassess;
using EventBus.Base.Abstraction;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Bidder.BidService.Application.Features.Command.Bidding.CreateBid
{
    public class CreateBidCommandHandler : IRequestHandler<CreateBidCommand, ResponseMessage<CreateBidResponse>>
    {

        private readonly IUnitOfWork uof;
        private readonly ILogger<CreateBidCommandHandler> logger;
        private readonly IEventBus eventBus;
        private readonly IIdentityService identityService;

        public CreateBidCommandHandler(IUnitOfWork uof, ILogger<CreateBidCommandHandler> logger, IEventBus eventBus, IIdentityService identityService)
        {
            this.uof = uof;
            this.logger = logger;
            this.eventBus = eventBus;
            this.identityService = identityService;
        }

        public async Task<ResponseMessage<CreateBidResponse>> Handle(CreateBidCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Bid bid = new(request.Title, request.Description, request.MinPrice, request.HasIncreaseRest, request.MinPriceIncrease, identityService.GetUserId());

                await uof.BidRepository.Add(bid);
                await uof.SaveChangesAsync();
                return ResponseMessage<CreateBidResponse>.Success(new CreateBidResponse(bid.Id), 200);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.StackTrace);
                return ResponseMessage<CreateBidResponse>.Fail("Bid Creation Failed", 500);
            }
        }
    }
}
