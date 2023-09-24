using Bidder.BidService.Application.Interfaces.Repos;
using Bidder.BidService.Domain.Entities;
using Bidder.BidService.Infastructure.Context;

namespace Bidder.BidService.Infastructure.Repos
{
    public class BidRoomRepository : Repository<BidRoom>, IBidRoomRepository
    {
        public BidRoomRepository(BidDbContext ctx) : base(ctx) { }
    } 
}
