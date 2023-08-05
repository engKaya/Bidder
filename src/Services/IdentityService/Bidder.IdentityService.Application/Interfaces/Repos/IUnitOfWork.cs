namespace Bidder.IdentityService.Application.Interfaces.Repos
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; } 
        Task<bool> SaveChangesAsync(CancellationToken cancellation = default);
    }
}
