﻿using Bidder.BidService.Domain.Entities;
using Bidder.Domain.Common.Bid.Enums;

namespace Bidder.BidService.Domain.DTOs.Bidding.GetBidRoom
{
    public class GetBidRoomResponse
    {
        public long RoomId { get; set; }
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
