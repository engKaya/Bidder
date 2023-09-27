using Bidder.BidService.Application.Interfaces.Services;
using Bidder.BidService.Grpc.Protos;
using Bidder.Domain.Common.Bid.Enums;
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
            try
            { 
                response.BidId = result.Data.Id.ToString();
                response.BidEndDate = result.Data.EndDate.ToUniversalTime().ToTimestamp();
                response.BidStatus = (int)result.Data.BidRoom.RoomStatus;
                response.RoomId = result.Data.BidRoom.Id.ToString();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return response;
        }
    }
}
