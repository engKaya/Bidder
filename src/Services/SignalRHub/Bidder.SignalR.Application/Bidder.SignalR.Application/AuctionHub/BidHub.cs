using Bidder.Application.Common.Redis.Interface;
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
