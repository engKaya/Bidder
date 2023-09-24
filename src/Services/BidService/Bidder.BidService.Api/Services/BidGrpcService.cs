using Bidder.BidService.Application.Interfaces.Services;
using Bidder.BidService.Grpc.Protos;
using EventBus.Base.Abstraction;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System.Net;

namespace Bidder.BidService.Api.Services
{
    public class BidGrpcService : Bidder.BidService.Grpc.Protos.BidGrpcService.BidGrpcServiceBase
    { 
        private readonly IEventBus eventBus;
        private readonly IBiddingService bidService;

        public BidGrpcService(IEventBus eventBus, IBiddingService bidService)
        {
            this.eventBus = eventBus;
            this.bidService = bidService;
        }

        public override async Task<GetBidRoomResponse> GetBidRoom(GetBidRoomRequest request, ServerCallContext context)
        {
            var result = await bidService.GetBidRoom(Guid.Parse(request.Id));
            var response = new GetBidRoomResponse();

            if (result.StatusCode == (int)HttpStatusCode.NotFound)
                return response;


            response.BidId = result.Data.BidId.ToString();
            response.BidEndDate = Timestamp.FromDateTime(result.Data.EndDate.ToLocalTime());
            response.BidStatus = (int)result.Data.BidRoomStatus;
            response.RoomId = result.Data.RoomId.ToString();



            return response;
        }
    }
}
