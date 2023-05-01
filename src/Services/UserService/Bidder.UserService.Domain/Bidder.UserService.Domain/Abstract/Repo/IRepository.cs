using Bidder.UserService.Domain.Abstract.UnitOfWork;
using Bidder.UserService.Domain.Extensions;

namespace Bidder.UserService.Domain.Abstract.Repo
{
    public interface IRepository<T> where T : BaseEntity
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
