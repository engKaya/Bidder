using Bidder.Application.Common.Extension;
using Bidder.Application.Common.Redis.Interface;
using Bidder.Domain.Common.Bid.Enums;
using Bidder.Domain.Common.Dto.BidService.IBiddingService;
using Bidder.Infastructure.Common.Grpc;
using Bidder.Infastructure.Common.Protos.Common;
using Bidder.SignalR.Domain.DTO.Responses.Join;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
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
            logger.LogInformation("Client joined", Context.ConnectionId.ToString());
            using var grpcChannel = GrpcClientFactory.GrpcChannelFactory(GrpcServerType.BiddingGrpcService);
            var client = new Infastructure.Common.Protos.Client.BidGrpcService.BidGrpcServiceClient(grpcChannel);
            GetBidRoomsGrpcResponse response = new();
            logger.LogInformation("Grpc Client Created");

            var policy = PollyPolicyGenerator.CreateExceptionPolicy(logger);

            await policy.ExecuteAsync(async () =>
            {
                logger.LogInformation("Calling Grpc Service GetBidRoomRequest");
                response = await client.GetBidRoomAsync(new GetBidRoomGrpcRequest() { Id = BidId.ToString() });
                logger.LogInformation($"Grpc Service GetBidRoomRequest Completed, Response:{(response == null ? "null" : response.ToString())}");
            });

            if (response == null || response.BidStatus == (int)BidRoomStatus.NeverCreated)
            {
                logger.LogInformation($"BidRoomStatus:{response.BidStatus}, Exiting");
                return new JoinResponse(HttpStatusCode.NotFound, "BID_NOT_FOUND", Context.ConnectionId);}



            if (response.BidStatus == (int)BidRoomStatus.Created)
            {
                logger.LogInformation($"BidRoomStatus:{response.BidStatus}, Joining {response.BidId} group with {Context.ConnectionId}");
                await Groups.AddToGroupAsync(Context.ConnectionId, response.BidId);
                logger.LogInformation("Joined Group looking in cache for BidRoom");
                var bidRoom = cacheService.Get<ActiveBidRoom>(response.BidId.ToString());
                if (bidRoom is null)
                { 
                }
            }

            return new JoinResponse(HttpStatusCode.OK, "JOINED", Context.ConnectionId);  
        }

        private async Task<ActiveBidRoomGrpcResponse> FindAndSetRedisActiveBidRoom(string BidId)
        {

            using var grpcChannel = GrpcClientFactory.GrpcChannelFactory(GrpcServerType.BiddingGrpcService);
            var client = new Infastructure.Common.Protos.Client.BidGrpcService.BidGrpcServiceClient(grpcChannel);

            GetActiveBidRoomGrpcRequest request = new();

            request.BidId = BidId;
            var response = await client.GetActiveBidRoomAsync(request); 
            return response;
        }
    }
}
