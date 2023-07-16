using Bidder.UserService.Application.Interfaces.Repo;
using Bidder.UserService.Domain.Models;

namespace Bidder.UserService.Application.Interfaces.Repos
{
    public interface IUserRepository : IGenericRepository<User>
    {
    }
}
