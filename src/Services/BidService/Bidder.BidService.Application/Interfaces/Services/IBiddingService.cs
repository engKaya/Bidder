﻿using Bidder.BidService.Application.Features.Command.Bidding.CreateBid;
using Bidder.BidService.Domain.DTOs.Bidding.CreateBid;
using Bidder.BidService.Domain.DTOs.Bidding.GetBidRoom;
using Bidder.BidService.Domain.Entities;
using Bidder.Domain.Common.BaseClassess;

namespace Bidder.BidService.Application.Interfaces.Services
{
    public interface IBiddingService
    {
        public Task<ResponseMessage<CreateBidResponse>> CreateBid(Bid request, CancellationToken cancellationToken);
        public Task<ResponseMessage<CreateBidResponse>> CreateBid(CreateBidCommand request, CancellationToken cancellationToken); 
        public Task<ResponseMessage<GetBidRoomResponse>> GetBidRoom(Guid BidId);
    }
}
