using Bidder.Domain.Common.Entity;
using Bidder.Domain.Common.Interfaces;
using Bidder.IdentityService.Infastructure.Context;
using Bidder.Infastructure.Common.Repos;
using Microsoft.EntityFrameworkCore;

namespace Bidder.IdentityService.Infastructure.Repos
{
    public class Repository<T> : BaseRepository<T>, IBaseRepository<T> where T : BaseEntity
    { 
        public Repository(UserDbContext context) : base(context)
        {
        }
    }
}
