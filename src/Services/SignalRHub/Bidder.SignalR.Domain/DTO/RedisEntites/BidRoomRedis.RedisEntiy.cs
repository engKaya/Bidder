using Bidder.Domain.Common.Bid.Enums;
using Bidder.Infastructure.Common.Protos.Client;

namespace Bidder.SignalR.Domain.DTO.RedisEntites
{
    public class BidRoomRedis
    {
        public Guid BidId { get;  }
        public long RoomId { get;  }
        public BidRoomStatus BidStatus { get;  }
        public List<RoomUser>? RoomUsers { get; } 
        public DateTime EndDate { get;  } 


        public BidRoomRedis(Guid bidId, long roomId, BidRoomStatus bidStatus, DateTime endDate)
        {
            BidId = bidId;
            RoomId = roomId;
            BidStatus = bidStatus;
            EndDate = endDate;
        }

        public BidRoomRedis()
        {
        } 
        public BidRoomRedis(GetBidRoomsResponse response ) { 
            this.RoomId = (long)response.RoomId;
            this.BidId = Guid.Parse(response.BidId);
            this.BidStatus = (BidRoomStatus)response.BidStatus;
            this.EndDate = response.BidEndDate.ToDateTime(); 
        } 
    }
}
