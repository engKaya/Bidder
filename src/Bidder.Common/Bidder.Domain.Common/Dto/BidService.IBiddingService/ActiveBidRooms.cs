using Bidder.Domain.Common.Bid.Enums;

namespace Bidder.Domain.Common.Dto.BidService.IBiddingService
{
    public class ActiveBidRooms
    {
        public Guid BidId { get; set; }
        public long RoomId { get; set; }
        public DateTime BidEndDate { get; set; }
        public BidRoomStatus BidRoomStatus { get; set; }
        public ActiveBidRooms()
        {

        }
        public ActiveBidRooms(Guid bidId, long roomId, DateTime bidEndDate, BidRoomStatus bidRoomStatus)
        {
            BidId = bidId;
            RoomId = roomId;
            BidEndDate = bidEndDate;
            BidRoomStatus = bidRoomStatus;
        }
    }
}
