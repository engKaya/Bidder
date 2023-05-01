using Bidder.UserService.Domain.Abstract.Repo;
using Bidder.UserService.Domain.Models;
using Bidder.UserService.Infastructure.Context;

namespace Bidder.UserService.Infastructure.Repos
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(UserDbContext context) : base(context)
        {
        }
    }
}
