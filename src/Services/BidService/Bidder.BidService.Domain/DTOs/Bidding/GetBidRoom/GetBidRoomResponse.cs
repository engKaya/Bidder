using Bidder.BidService.Domain.Entities;
using Bidder.BidService.Domain.Enums;

namespace Bidder.BidService.Domain.DTOs.Bidding.GetBidRoom
{
    public class GetBidRoomResponse
    {
        public Guid RoomId { get; set; }
        public Guid BidId { get; set; }
        public BidRoomStatus BidRoomStatus { get; set; }
        public DateTime EndDate { get; set; }

        public GetBidRoomResponse(BidRoom room)
        {
            this.RoomId = room.Id;
            this.BidId = room.Bid.Id;
            this.BidRoomStatus = room.RoomStatus;
            this.EndDate = room.Bid.EndDate;
        }
    }
}
