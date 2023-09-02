﻿using Bidder.BidService.Domain.Entities;
using Bidder.Domain.Common.Interfaces;

namespace Bidder.BidService.Application.Interfaces.Repos
{
    public interface IBidRepository :  IRepository<Bid>
    {
    }
}