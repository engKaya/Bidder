using Bidder.BidService.Application.Interfaces.Services;
using Bidder.Domain.Common.Bid.Enums;
using Bidder.Infastructure.Common.Protos.Server;
using EventBus.Base.Abstraction;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System.Net;

namespace Bidder.BidService.Api.Services
{
    public class BidGrpcServerService : BidGrpcService.BidGrpcServiceBase
    {
        private readonly IEventBus eventBus;
        private readonly IBiddingService bidService; 

        public BidGrpcServerService(IEventBus eventBus, IBiddingService bidService)
        {
            this.eventBus = eventBus;
            this.bidService = bidService;
        }

        public override async Task<GetBidRoomResponse> GetBidRoom(GetBidRoomRequest request, ServerCallContext context)
        {
            var result = await bidService.GetBid(Guid.Parse(request.Id));
            var response = new GetBidRoomResponse();
            if (result.StatusCode == (int)HttpStatusCode.NotFound)
            {
                response.BidStatus = (int)BidRoomStatus.NeverCreated;
                return response;
            }

            if (result.Data.EndDate > DateTime.Now && result.Data.BidRoom is null)
            {
                var createdRoom = await bidService.CreateBidRoom(result.Data, context.CancellationToken);
                response.BidStatus = (int)BidRoomStatus.Created;
                result.Data.BidRoom = createdRoom.Data;
            }
            else if (result.Data.EndDate <= DateTime.Now && result.Data.BidRoom is null)
            {
                response.BidStatus = (int)BidRoomStatus.NeverCreated;
                return response;
            }
                response.BidId = result.Data.Id.ToString();
                response.BidEndDate = result.Data.EndDate.ToUniversalTime().ToTimestamp();
                response.BidStatus = (int)result.Data.BidRoom.RoomStatus;
                response.RoomId = result.Data.BidRoom.Id.ToString();

            return response;
        }

        public override async Task<GetActiveBidRoomResponse> GetActiveBidRoom(Empty request, ServerCallContext context)
        { 
            var response = await bidService.GetActiveBidRooms();
            var result = new GetActiveBidRoomResponse();

            if (response.Data is null)
            {
                return result;
            }

            foreach (var item in response.Data)
            {
                result.ActiveBidRooms.Add(new ActiveBidRooms
                {
                    BidId = item.BidId.ToString(),
                    BidEndDate = item.BidEndDate.ToUniversalTime().ToTimestamp(),
                    BidStatus = (int)item.BidRoomStatus,
                    RoomId = item.RoomId
                });
            }

            return result;

        }

    }
}
