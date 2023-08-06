namespace Bidder.IdentityService.Application.Interfaces.Repos
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; } 
        Task<int> SaveChangesAsync(CancellationToken cancellation = default);
    }
}
