namespace Bidder.IdentityService.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public Task<int> SaveChangesAsync(CancellationToken cancellation = default(CancellationToken));
        public Task<bool> SaveEntitiesAsync(CancellationToken cancellation = default(CancellationToken));
    }
}
