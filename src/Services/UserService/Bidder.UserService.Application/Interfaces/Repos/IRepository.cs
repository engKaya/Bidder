using Bidder.UserService.Application.Interfaces.UnitOfWork;
using Bidder.UserService.Domain.Extensions;

namespace Bidder.UserService.Application.Interfaces.Repos
{
    public interface IRepository<T> where T : BaseEntity
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
