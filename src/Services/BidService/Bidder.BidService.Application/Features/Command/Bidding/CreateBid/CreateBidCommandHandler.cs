using Bidder.BidService.Application.Interfaces.Services;
using Bidder.BidService.Domain.DTOs.Bidding.CreateBid;
using Bidder.Domain.Common.BaseClassess;
using EventBus.Base.Abstraction;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Bidder.BidService.Application.Features.Command.Bidding.CreateBid
{
    public class CreateBidCommandHandler : IRequestHandler<CreateBidCommand, ResponseMessage<CreateBidResponse>>
    {

        private readonly ILogger<CreateBidCommandHandler> logger;
        private readonly IEventBus eventBus;
        private readonly IBiddingService bidService;

        public CreateBidCommandHandler(ILogger<CreateBidCommandHandler> logger, IEventBus eventBus, IBiddingService bidService)
        {
            this.logger = logger;
            this.eventBus = eventBus;
            this.bidService = bidService;
        }

        public async Task<ResponseMessage<CreateBidResponse>> Handle(CreateBidCommand request, CancellationToken cancellationToken)
        { 
            return await bidService.CreateBid(request, cancellationToken);
        }
    }
}
