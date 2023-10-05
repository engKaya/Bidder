using Bidder.Domain.Common.Bid.Enums;

namespace Bidder.Domain.Common.Dto.BidService.IBiddingService
{
    public class ActiveBidRoom
    {
        public Guid BidId { get; set; }
        public long RoomId { get; set; }
        public DateTime BidEndDate { get; set; }
        public BidRoomStatus BidRoomStatus { get; set; } 
        public IDictionary<Guid,string> Users { get ; set; } = new Dictionary<Guid,string>();
        public ActiveBidRoom()
        {

        }
        public ActiveBidRoom(string bidId, long roomId, DateTime bidEndDate, BidRoomStatus bidRoomStatus)
        {
            BidId = Guid.Parse(bidId);
            RoomId = roomId;
            BidEndDate = bidEndDate;
            BidRoomStatus = bidRoomStatus;
        }
    }
}
