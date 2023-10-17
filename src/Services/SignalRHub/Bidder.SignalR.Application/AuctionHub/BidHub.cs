using Bidder.Application.Common.Extension;
using Bidder.Application.Common.Interfaces;
using Bidder.Application.Common.Redis.Interface;
using Bidder.Domain.Common.Bid.Enums;
using Bidder.Domain.Common.Dto.BidService.IBiddingService;
using Bidder.Infastructure.Common.Grpc;
using Bidder.Infastructure.Common.Protos.Client;
using Bidder.Infastructure.Common.Protos.Common;
using Bidder.SignalR.Application.Redis.Interface;
using Bidder.SignalR.Domain.DTO.Responses.Join;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Bidder.SignalR.Application.AuctionHub
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BidHub : Hub
    {
        private readonly ILogger<BidHub> logger;
        private readonly IRoomRedisService roomRedisService;
        private readonly IIdentityService identityService;

        public BidHub(ILogger<BidHub> logger, IRoomRedisService roomRedisService, IIdentityService identityService)
        {
            this.logger = logger;
            this.roomRedisService = roomRedisService;
            this.identityService = identityService;
        }

        public override Task OnConnectedAsync()
        {

            logger.LogInformation("Client connected", Context.ToString());
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            logger.LogInformation("Client disconnected", Context.ToString());
            var userId = identityService.GetUserId();
            return base.OnDisconnectedAsync(exception);
        }

        public async Task<JoinResponse> Join(Guid BidId)
        {
            logger.LogInformation("Client joined", Context.ConnectionId.ToString());
            using var grpcChannel = GrpcClientFactory.GrpcChannelFactory(GrpcServerType.BiddingGrpcService);
            var client = new BidGrpcService.BidGrpcServiceClient(grpcChannel);
            GetBidRoomsGrpcResponse response = new();
            logger.LogInformation("Grpc Client Created");

            var policy = PollyPolicyGenerator.CreateExceptionPolicy(logger);

            await policy.ExecuteAsync(async () =>
            {
                logger.LogInformation("Calling Grpc Service GetBidRoomRequest");
                response = await client.GetBidRoomAsync(new GetBidRoomGrpcRequest() { Id = BidId.ToString() }); 
                var resp = response?.ToString() ?? "";
                logger.LogInformation("Grpc Service GetBidRoomRequest Completed, Response: {resp}", resp);
            });

            if (response == null || response.BidStatus == (int)BidRoomStatus.NeverCreated)
            {
                var status = response?.BidStatus;
                logger.LogInformation("BidRoomStatus:{status}, Exiting", status);
                return new JoinResponse(HttpStatusCode.NotFound, "BID_NOT_FOUND", Context.ConnectionId);
            }



            if (response.BidStatus == (int)BidRoomStatus.Created)
            {

                var status = response.BidStatus;
                var bidId = response.BidId;
                var conId = Context.ConnectionId;
                logger.LogInformation("BidRoomStatus: {status}, Joining {bidId} group with {conId}", status, bidId, conId);


                await Groups.AddToGroupAsync(Context.ConnectionId, response.BidId);

                logger.LogInformation("Joined Group looking in cache for BidRoom");
                var bidRoom = await roomRedisService.GetRoom(Guid.Parse(response.BidId));
                if (bidRoom is null)
                {
                    logger.LogInformation("Room {bidId} coludn't found in cache. Calling Grpc \"GetActiveBidRoomAsync\"", bidId);
                    bidRoom = await FindAndSetRedisActiveBidRoom(response.BidId, identityService.GetUserId(), Context.ConnectionId);
                }
                else
                {
                    var user = bidRoom.Users.FirstOrDefault(x => x.Key == identityService.GetUserId());
                    if (string.IsNullOrEmpty(user.Value))
                    {
                        bidRoom.Users.Add(identityService.GetUserId(), Context.ConnectionId);
                        await roomRedisService.CreateOrUpdateRoom(bidRoom);
                    }
                }
            }

            return new JoinResponse(HttpStatusCode.OK, "JOINED", Context.ConnectionId);
        }

        private async Task<ActiveBidRoomGrpcResponse> GetActiveBidRoomWithGrpcAsync(string BidId)
        {

            using var grpcChannel = GrpcClientFactory.GrpcChannelFactory(GrpcServerType.BiddingGrpcService);
            var client = new Infastructure.Common.Protos.Client.BidGrpcService.BidGrpcServiceClient(grpcChannel);

            GetActiveBidRoomGrpcRequest request = new() { BidId = BidId };
            var response = await client.GetActiveBidRoomAsync(request);
            return response;
        }
        private async Task<ActiveBidRoom> FindAndSetRedisActiveBidRoom(string BidId, Guid UserId, string ConnectionId)
        {
            var grpcResponse = await GetActiveBidRoomWithGrpcAsync(BidId);
            if (grpcResponse == null)
            {
                return null;
            }

            ActiveBidRoom room = new(grpcResponse.BidId, grpcResponse.RoomId, grpcResponse.Title, grpcResponse.Description, Guid.Parse(grpcResponse.OwnerId), grpcResponse.BidEndDate.ToDateTime(), (BidRoomStatus)grpcResponse.BidStatus);
            room.Users.Add(UserId, ConnectionId);
            await roomRedisService.CreateOrUpdateRoom(room);
            return room;
        }

        public async Task SendMessage(string BidId, string connectionId, string message)
        {
            await Clients.GroupExcept(BidId, new List<string> { connectionId }).SendAsync("ReceiveMessage", message);
        }
    }
}
