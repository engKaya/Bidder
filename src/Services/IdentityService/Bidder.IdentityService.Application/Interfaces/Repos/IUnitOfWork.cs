using Bidder.Application.Common.Interfaces;

namespace Bidder.IdentityService.Application.Interfaces.Repos
{
    public interface IUnitOfWork : IBaseUnitOfWork
    {
        IUserRepository UserRepository { get; } 
    }
}
