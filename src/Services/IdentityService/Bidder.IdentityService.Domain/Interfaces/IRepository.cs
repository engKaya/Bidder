using Bidder.IdentityService.Domain.Entities;

namespace Bidder.IdentityService.Domain.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
