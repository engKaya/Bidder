using Bidder.Application.Common.Interfaces;
using Bidder.BidService.Application.Features.Command.Bidding.CreateBid;
using Bidder.BidService.Application.Interfaces.Repos;
using Bidder.BidService.Application.Interfaces.Services;
using Bidder.BidService.Domain.DTOs.Bidding.CreateBid;
using Bidder.BidService.Domain.Entities;
using Bidder.Domain.Common.BaseClassess;
using Bidder.Domain.Common.Dto.BidService.IBiddingService;
using Microsoft.Extensions.Logging;
using System.Threading;

namespace Bidder.BidService.Infastructure.Services
{
    public class BiddingService : IBiddingService
    {
        private readonly IUnitOfWork uof; 
        private readonly IIdentityService identityService;
        private readonly ILogger<BiddingService> logger;

        public BiddingService(IUnitOfWork uof,  IIdentityService identityService, ILogger<BiddingService> logger)
        {
            this.uof = uof; 
            this.identityService = identityService;
            this.logger = logger;

        }

        public async Task<ResponseMessage<CreateBidResponse>> CreateBid(Bid request, CancellationToken cancellationToken)
        {
            try
            {
                await uof.BidRepository.Add(request, cancellationToken);
                await CreateBidRoom(request, cancellationToken);
                return ResponseMessage<CreateBidResponse>.Success(new CreateBidResponse(request.Id), 200);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.StackTrace);
                return ResponseMessage<CreateBidResponse>.Fail("Bid Creation Failed", 500);
            }
        }

        public Task<ResponseMessage<CreateBidResponse>> CreateBid(CreateBidCommand request, CancellationToken cancellationToken)
        {
            Bid bid = new(request.Title, request.Description, request.MinPrice, request.HasIncreaseRest, request.MinPriceIncrease, identityService.GetUserId());
            return CreateBid(bid, cancellationToken);
        }

        public async Task<ResponseMessage<BidRoom>> CreateBidRoom(Bid bid, CancellationToken cancellationToken = default)
        {
            var room = new BidRoom(bid);
            await uof.BidRoomRepository.Add(room, cancellationToken);
            return ResponseMessage<BidRoom>.Success(room, 200);
        }

        public async Task<ResponseMessage<BidRoom>> UpdateBidRoom(BidRoom room)
        { 
            uof.BidRoomRepository.Update(room);
            await uof.SaveChangesAsync();
            return ResponseMessage<BidRoom>.Success(room, 200);
        }

        public async Task<ResponseMessage<ActiveBidRoom>> GetActiveBidRoom(string BidId, CancellationToken cancellationToken)
        { 
            var bid = await uof.BidRepository.FindFirst(x => x.Id == Guid.Parse(BidId), null, cancellationToken, x => x.BidRoom);
            ActiveBidRoom room = new(bid.BidRoom.BidId.ToString(), bid.BidRoom.Id, bid.Title, bid.Description, bid.UserId, bid.EndDate, bid.BidRoom.RoomStatus);
            return ResponseMessage<ActiveBidRoom>.Success(room, 200);
        }

        public async Task<ResponseMessage<IEnumerable<ActiveBidRoom>>> GetActiveBidRooms(CancellationToken cancellationToken)
        {
            try
            {
                List<ActiveBidRoom> activeRooms = new();
                IEnumerable<Bid> bids = await uof.BidRepository.GetWhere(x => !x.IsEnded && x.EndDate > DateTime.Now, null, cancellationToken, x => x.BidRoom);
                foreach (var bid in bids)
                {
                    
                    if (bid.BidRoom is null || bid.BidRoom.Id == 0)
                        continue;

                    ActiveBidRoom room = new(bid.BidRoom.BidId.ToString(), bid.BidRoom.Id, bid.Title, bid.Description, bid.UserId, bid.EndDate, bid.BidRoom.RoomStatus);
                    activeRooms.Add(room);
                }   
                return ResponseMessage<IEnumerable<ActiveBidRoom>>.Success(activeRooms.AsEnumerable()); 
            }
            catch (Exception ex)
            {
                logger.LogError("Failed On GetActiveBidRooms, Stack: {stack}", ex.StackTrace); 
                return ResponseMessage<IEnumerable<ActiveBidRoom>>.Fail("Failed On GetActiveBidRooms", 500);
            }
        }

        public async Task<ResponseMessage<Bid>> GetBid(Guid id, CancellationToken cancellationToken)
        {
            var room = await uof.BidRepository.FindFirst(x => x.Id == id, null, cancellationToken, x => x.BidRoom);

            if (room is null)
                return ResponseMessage<Bid>.Fail("BID_NOT_FOUND", 404);

            return ResponseMessage<Bid>.Success(room, 200);
        }

        public async Task<ResponseMessage<BidRoom>> GetActiveBidRoom(long RoomId, CancellationToken cancellationToken)
        {
            var room = await uof.BidRoomRepository.FindFirst(x => x.Id == RoomId, null, cancellationToken);
            if (room is null)
                return ResponseMessage<BidRoom>.Fail("ROOM_NOT_FOUND", 404);

            return ResponseMessage<BidRoom>.Success(room);
        }

        public async Task<ResponseMessage<IEnumerable<Bid>>> GetAllBids(CancellationToken cancellationToken)
        {
            var bids = await uof.BidRepository.GetAll();
            return ResponseMessage<IEnumerable<Bid>>.Success(bids.AsEnumerable());
        }
    }
}
