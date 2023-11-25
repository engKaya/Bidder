using Bidder.BidService.Application.Features.Command.Bidding.GetBid;
using Bidder.BidService.Application.Features.Command.Bidding.UpdateBidRoom;
using Bidder.BidService.Application.Features.Queries.GetActiveBidRoom;
using Bidder.BidService.Application.Features.Queries.GetActiveBidRooms;
using Bidder.BidService.Application.Interfaces.Services;
using Bidder.BidService.Domain.Entities;
using Bidder.Domain.Common.Bid.Enums;
using Bidder.Infastructure.Common.Protos.Common;
using Bidder.Infastructure.Common.Protos.Server;
using EventBus.Base.Abstraction;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using System.Net;

namespace Bidder.BidService.Api.GrpcServices
{
    public class BidGrpcServerService : BidGrpcService.BidGrpcServiceBase
    {
        private readonly IEventBus eventBus;
        private readonly IBiddingService bidService;
        private readonly IMediator mediator;
         

        public BidGrpcServerService(IEventBus eventBus, IBiddingService bidService, IMediator mediator)
        {
            this.eventBus = eventBus;
            this.bidService = bidService;
            this.mediator = mediator;
        }

        public override async Task<GetBidRoomsGrpcResponse> GetBidRoom(GetBidRoomGrpcRequest request, ServerCallContext context)
        {
            var result = await mediator.Send(new GetBidQuery(Guid.Parse(request.Id)), context.CancellationToken);
            var response = new GetBidRoomsGrpcResponse();
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

        public override async Task<GetActiveBidRoomsGrpcResponse> GetActiveBidRooms(Empty request, ServerCallContext context)
        { 
            var response = await mediator.Send(new GetActiveBidRoomsQuery());
            var result = new GetActiveBidRoomsGrpcResponse();

            if (response.Data is null)
                return result;
             
            foreach (var item in response.Data)
            {
                result.ActiveBidRooms.Add(new ActiveBidRoomGrpcResponse
                {
                    BidId = item.BidId.ToString(),
                    BidEndDate = item.BidEndDate.ToUniversalTime().ToTimestamp(),
                    BidStatus = (int)item.BidRoomStatus,
                    RoomId = item.RoomId,
                    Title = item.Title,
                    Description = item.Description,
                    OwnerId = item.OwnerId.ToString()
                });
            }   
            return result;
        }

        public override async Task<ActiveBidRoomGrpcResponse> GetActiveBidRoom(GetActiveBidRoomGrpcRequest request, ServerCallContext context)
        {
            var serviceresponse = await bidService.GetActiveBidRoom(request.BidId, context.CancellationToken);
            
            var response = new ActiveBidRoomGrpcResponse();

            if (serviceresponse.Data is null)
                return response;

            response.BidId = serviceresponse.Data.BidId.ToString();
            response.RoomId= serviceresponse.Data.RoomId;
            response.BidStatus = (int)serviceresponse.Data.BidRoomStatus;
            response.BidEndDate = serviceresponse.Data.BidEndDate.ToUniversalTime().ToTimestamp();
            response.OwnerId= serviceresponse.Data.OwnerId.ToString();

            return response;
        }

        public override async Task<UpdateBidRoomStatusGrpcResponse> UpdateBidRoomStatus(UpdateBidRoomStatusGrpcRequest request, ServerCallContext context)
        {
            var response = new UpdateBidRoomStatusGrpcResponse();
            var bidRoomResponse = await mediator.Send(new GetActiveBidRoomQuery(request.RoomId), context.CancellationToken);
            
            if (bidRoomResponse.StatusCode == (int)HttpStatusCode.NotFound)
            {
                response.RoomStatus = (int)BidRoomStatus.NeverCreated;
                return response;
            }

            BidRoom data = bidRoomResponse.Data;
            data.RoomStatus = (BidRoomStatus)request.RoomStatus;

            var result = await mediator.Send(new UpdateBidRoomCommand(data), context.CancellationToken);
            response.RoomStatus = (int)result.Data.RoomStatus;
            response.RoomId = result.Data.Id;
            return response;
        }
    }
}
