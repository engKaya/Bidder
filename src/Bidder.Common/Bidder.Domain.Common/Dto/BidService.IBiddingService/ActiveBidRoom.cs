using Bidder.Domain.Common.Bid.Enums;

namespace Bidder.Domain.Common.Dto.BidService.IBiddingService
{
    public class ActiveBidRoom
    {
        public Guid BidId { get; set; }
        public long RoomId { get; set; }
        public DateTime BidEndDate { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid OwnerId { get; set; }
        public BidRoomStatus BidRoomStatus { get; set; } 
        public IDictionary<Guid,string> Users { get ; set; } = new Dictionary<Guid,string>();
        public ActiveBidRoom()
        {

        }
        public ActiveBidRoom(string bidId, long roomId, string title, string description, Guid ownerId, DateTime bidEndDate, BidRoomStatus bidRoomStatus)
        {
            BidId = Guid.Parse(bidId);
            RoomId = roomId;
            BidEndDate = bidEndDate;
            BidRoomStatus = bidRoomStatus;
            Title = title;
            Description = description;
            OwnerId = ownerId;
        }
    }
}
