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

        public override async Task<GetBidRoomsResponse> GetBidRoom(GetBidRoomRequest request, ServerCallContext context)
        {
            var result = await bidService.GetBid(Guid.Parse(request.Id));
            var response = new GetBidRoomsResponse();
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
                response.RoomId = result.Data.BidRoom.Id;

            return response;
        }

        public override async Task<GetActiveBidRoomsResponse> GetActiveBidRooms(Empty request, ServerCallContext context)
        { 
            var response = await bidService.GetActiveBidRooms();
            var result = new GetActiveBidRoomsResponse();

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

        public override async Task<ActiveBidRooms> GetActiveBidRoom(GetActiveBidRoomRequest request, ServerCallContext context)
        {
            var serviceresponse = await bidService.GetActiveBidRoom(request.BidId);
            
            var response = new ActiveBidRooms();

            if (serviceresponse.Data is null)
                return response;

            response.BidId = serviceresponse.Data.BidId.ToString();
            response.RoomId= serviceresponse.Data.RoomId;
            response.BidStatus = (int)serviceresponse.Data.BidRoomStatus;
            response.BidEndDate = serviceresponse.Data.BidEndDate.ToUniversalTime().ToTimestamp();

            return response;
        }
    }
}
