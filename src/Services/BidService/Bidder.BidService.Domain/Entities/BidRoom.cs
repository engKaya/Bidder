using Bidder.Domain.Common.Bid.Enums;
using Bidder.Domain.Common.Entity;

namespace Bidder.BidService.Domain.Entities
{
    public class BidRoom : BaseEntity
    {
        private Guid _bidId;
        private string _bidName;
        private BidRoomStatus _roomStatus;
        private Bid _bid;
        private List<BidRoomUser> _bidRoomUsers = new List<BidRoomUser>();


        public Guid BidId { get => _bidId; set => _bidId = value; }
        public string BidName { get => _bidName; set => _bidName = value; }
        public BidRoomStatus RoomStatus { get => _roomStatus; set => _roomStatus = value; }
        public List<BidRoomUser> BidRoomUsers { get => _bidRoomUsers; }
        public Bid Bid { get => _bid; set => _bid = value; }
        public BidRoom()
        {

        }
        public BidRoom(Bid bid)
        {
            this.BidId = bid.Id;
            this.BidName = bid.Title;
            this.RoomStatus = BidRoomStatus.Created;
            this.Bid = bid;
        }
    }
}
