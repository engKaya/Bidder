using Azure;
using Bidder.Application.Common.Redis.Interface;
using Bidder.Infastructure.Common.Grpc;
using Bidder.SignalR.Application.Protos;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

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

        public async Task<string> Join()
        {
            using var grpcChannel = GrpcClientFactory.GrpcChannelFactory(GrpcServerType.BiddingGrpcService);
            var client = new BidGrpcService.BidGrpcServiceClient(grpcChannel);
            GetBidRoomResponse response;
            try
            {
                response = await client.GetBidRoomAsync(new GetBidRoomRequest() { Id = Guid.NewGuid().ToString() });

            }
            catch (Exception ex)
            {
                logger.LogTrace("Error ", ex.StackTrace);
                throw;
            }

            if (response == null)
            {
                throw new Exception("Bid room not found");
            }

            return response.BidId;    
        }
    }
}
