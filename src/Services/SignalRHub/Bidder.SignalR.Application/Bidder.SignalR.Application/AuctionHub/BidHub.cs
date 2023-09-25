using Bidder.Application.Common.Redis.Interface;
using Bidder.Infastructure.Common.Grpc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Bidder.SignalR.Application.AuctionHub
{
    public class BidHub  : Hub
    {
        private readonly ILogger<BidHub> logger;
        private readonly IDistributedCacheManager cacheService;

        public BidHub(ILogger<BidHub> logger, IDistributedCacheManager cacheService)
        {
            this.logger = logger;
            this.cacheService = cacheService;
        }

        public override Task OnConnectedAsync()
        {
            using var grpcChannel = GrpcClientFactory.GrpcChannelFactory(GrpcServerType.BiddingGrpcService);
            var client = BidGrpcService


            logger.LogInformation("Client connected", Context.ToString());
            cacheService.Set("a", "b");
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            logger.LogInformation("Client disconnected", Context.ToString());
            return base.OnDisconnectedAsync(exception);
        }
    }
}
