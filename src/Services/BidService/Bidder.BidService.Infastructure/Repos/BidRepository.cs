using Bidder.BidService.Application.Interfaces.Repos;
using Bidder.BidService.Domain.Entities;
using Bidder.BidService.Infastructure.Context;

namespace Bidder.BidService.Infastructure.Repos
{
    public class BidRepository : Repository<Bid>, IBidRepository
    {
        public BidRepository(BidDbContext ctx)  : base(ctx) { }
    }
}
