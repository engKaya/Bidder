

using Bidder.IdentityService.Application.Interfaces.Repos;
using Bidder.IdentityService.Domain.Entities;
using Bidder.IdentityService.Infastructure.Context;

namespace Bidder.IdentityService.Infastructure.Repos
{
    public class UserRepository : Repository<Users>, IUserRepository
    {
        public UserRepository(UserDbContext context) : base(context)
        {
        }
    }
}
