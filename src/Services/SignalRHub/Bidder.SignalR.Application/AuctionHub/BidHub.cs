using Bidder.Application.Common.Redis.Interface;
using Bidder.Domain.Common.Bid.Enums;
using Bidder.Infastructure.Common.Grpc;
using Bidder.Infastructure.Common.Protos;
using Bidder.SignalR.Domain.DTO.Responses.Join;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using System.Net;

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

        public async Task<JoinResponse> Join(Guid BidId)
        {
            using var grpcChannel = GrpcClientFactory.GrpcChannelFactory(GrpcServerType.BiddingGrpcService);
            var client = new BidGrpcService.BidGrpcServiceClient(grpcChannel);
            GetBidRoomResponse response = new();


            var policy = CreateExceptionPolicy();

            await policy.ExecuteAsync(async () =>
            {
                response = await client.GetBidRoomAsync(new GetBidRoomRequest() { Id = BidId.ToString() });
            });

            if (response == null || response.BidStatus == (int)BidRoomStatus.NeverCreated)
            {
                return new JoinResponse(HttpStatusCode.NotFound, "Bid not found", Context.ConnectionId);
            }

            if (response.BidStatus == (int)BidRoomStatus.Created)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, response.BidId);
            }

            return new JoinResponse(HttpStatusCode.OK, "Joined Successfully", Context.ConnectionId);  
        }

        private AsyncRetryPolicy CreateExceptionPolicy()
        {
            return Policy
                .Handle<Exception>()
                .RetryAsync(3, (exception, count) =>
                {
                    logger.LogError(exception, "Error while getting bid room with Grpc");
                });
        }
    }
}
