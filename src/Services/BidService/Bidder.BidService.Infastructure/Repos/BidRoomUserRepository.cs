using Bidder.BidService.Application.Interfaces.Repos;
using Bidder.BidService.Domain.Entities;
using Bidder.BidService.Infastructure.Context;
using Bidder.Infastructure.Common.Repos;

namespace Bidder.BidService.Infastructure.Repos
{
    public class BidRoomUserRepository : BaseRepository<BidRoomUser>, IBidRoomUserRepository
    {
        public BidRoomUserRepository(BidDbContext context) : base(context)
        {
        }
    }
}
