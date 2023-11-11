using Bidder.Application.Common.Extension;
using Bidder.Domain.Common.BaseClassess;
using Bidder.Domain.Common.Bid.Enums;
using Bidder.Domain.Common.Dto.BidService.IBiddingService;
using Bidder.Infastructure.Common.Grpc;
using Bidder.Infastructure.Common.Protos.Client;
using Bidder.Infastructure.Common.Protos.Common;
using Bidder.SignalR.Application.Redis.Interface;
using Bidder.SignalR.Application.Services.Interface;
using Microsoft.Extensions.Logging;

namespace Bidder.SignalR.Application.Services.Implementation
{
    public class BidRoomService : IBidRoomService
    {
        private readonly IRoomRedisService _roomRedis;
        private readonly ILogger<BidRoomService> _logger;

        public BidRoomService(IRoomRedisService roomRedis, ILogger<BidRoomService> logger)
        {
            _roomRedis = roomRedis;
            _logger = logger;
        }

        public async Task<ResponseMessageNoContent> GetActiveRoomsAndSaveToRedis()
        {
            var response = await GetRoomsFromBidService();
            if (response == null || response.ActiveBidRooms.Count == 0) {
                _logger.LogInformation("GetActiveRoomsAndSaveToRedis has ended with no rooms");
                return ResponseMessageNoContent.Success();
            }

            _logger.LogInformation($"GetActiveRoomsAndSaveToRedis has ended with {response.ActiveBidRooms.Count} rooms");

            foreach (var room in response.ActiveBidRooms)
            {
                try
                { 
                    ActiveBidRoom bidroomredis = new(room.BidId, room.RoomId, room.Title, room.Description, Guid.Parse(room.OwnerId), room.BidEndDate.ToDateTime(), (BidRoomStatus)room.BidStatus);
                    await _roomRedis.CreateOrUpdateRoom(bidroomredis);
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex.StackTrace);
                }
            }

            return ResponseMessageNoContent.Success();
        }

        private async Task<GetActiveBidRoomsGrpcResponse> GetRoomsFromBidService()
        {
            try
            {  
                _logger.LogInformation("GetActiveRoomsAndSaveToRedis has started");

                using var channel = GrpcClientFactory.GrpcChannelFactory(GrpcServerType.BiddingGrpcService);
                var client = new BidGrpcService.BidGrpcServiceClient(channel);

                var policy = PollyPolicyGenerator.CreateExceptionPolicy(_logger);
                var response = await policy.ExecuteAsync<GetActiveBidRoomsGrpcResponse>(async () =>
                {
                    return await client.GetActiveBidRoomsAsync(new Google.Protobuf.WellKnownTypes.Empty());
                });

                _logger.LogInformation($"GetActiveRoomsAndSaveToRedis has finished with {response.ActiveBidRooms.Count}");
                return response;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
