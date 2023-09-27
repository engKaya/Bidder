using Bidder.Domain.Common.Bid.Enums;
using Bidder.SignalR.Domain.Protos;

namespace Bidder.SignalR.Domain.DTO.RedisEntites
{
    public class BidRoomRedis
    {
        public Guid BidId { get;  }
        public Guid RoomId { get;  }
        public BidRoomStatus BidStatus { get;  }
        public DateTime EndDate { get;  } 
        public BidRoomRedis(Guid bidId, Guid roomId, BidRoomStatus bidStatus, DateTime endDate)
        {
            BidId = bidId;
            RoomId = roomId;
            BidStatus = bidStatus;
            EndDate = endDate;
        }

        public BidRoomRedis()
        {
        } 
        public BidRoomRedis(GetBidRoomResponse response ) { 
            this.RoomId = Guid.Parse(response.RoomId);
            this.BidId = Guid.Parse(response.BidId);
            this.BidStatus = (BidRoomStatus)response.BidStatus;
            this.EndDate = response.BidEndDate.ToDateTime(); 
        } 
    }
}
