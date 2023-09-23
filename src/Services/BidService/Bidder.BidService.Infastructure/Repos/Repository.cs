using Bidder.BidService.Infastructure.Context;
using Bidder.Domain.Common.Entity;
using Bidder.Domain.Common.Interfaces;
using Bidder.Infastructure.Common.Repos;

namespace Bidder.BidService.Infastructure.Repos
{ 
    public class Repository<T> : BaseRepository<T>, IBaseRepository<T> where T : BaseEntity
    {
        public Repository(BidDbContext context) : base(context)
        {
        }
    }
} 
