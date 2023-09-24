using Bidder.BidService.Domain.Enums;
using Bidder.Domain.Common.Entity;

namespace Bidder.BidService.Domain.Entities
{
    public class BidRoom : BaseEntity
    { 
        public Guid BidId { get; set; }
        public string BidName { get; set; } = string.Empty;
        public BidRoomStatus RoomStatus { get; set; }
        public Bid Bid { get; set; } = null!;
    }
}
