using Bidder.BidService.Domain.Enums;
using Bidder.Domain.Common.Entity;
using System.Security.Cryptography.X509Certificates;

namespace Bidder.BidService.Domain.Entities
{
    public class BidRoom : BaseEntity
    { 
        public Guid BidId { get; set; }
        public string BidName { get; set; } = string.Empty;
        public BidRoomStatus RoomStatus { get; set; }
        public Bid Bid { get; set; } = null;

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
