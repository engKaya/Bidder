using Bidder.BidService.Application.Interfaces.Repos;
using Bidder.BidService.Application.Interfaces.Services;
using Bidder.BidService.Domain.DTOs.Bidding.CreateBid;
using Bidder.Domain.Common.BaseClassess;
using EventBus.Base.Abstraction;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Bidder.BidService.Application.Features.Command.Bidding.CreateBid
{
    public class CreateBidCommandHandler : IRequestHandler<CreateBidCommand, ResponseMessage<CreateBidResponse>>
    {

        private readonly ILogger<CreateBidCommandHandler> logger;
        private readonly IEventBus eventBus;
        private readonly IBiddingService bidService;
        private readonly IUnitOfWork unitOfWork;

        public CreateBidCommandHandler(ILogger<CreateBidCommandHandler> logger, IEventBus eventBus, IBiddingService bidService, IUnitOfWork unitOfWork)
        {
            this.logger = logger;
            this.eventBus = eventBus;
            this.bidService = bidService;
            this.unitOfWork = unitOfWork;
        }

        public async Task<ResponseMessage<CreateBidResponse>> Handle(CreateBidCommand request, CancellationToken cancellationToken)
        {
            try
            { 
                var response = await bidService.CreateBid(request, cancellationToken); 
                await unitOfWork.SaveChangesAsync(cancellationToken);
                return response;
            }
            catch (Exception ex)
            {
                logger.LogError("Ex At CreateBidCommandHandler", ex, ex.StackTrace);
                return ResponseMessage<CreateBidResponse>.Fail(ex);
            }
        }
    }
}
