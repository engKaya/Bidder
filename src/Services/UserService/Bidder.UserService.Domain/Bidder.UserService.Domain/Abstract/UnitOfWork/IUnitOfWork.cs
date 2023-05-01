namespace Bidder.UserService.Domain.Abstract.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> SaveChangesAsync(CancellationToken cancellation = default(CancellationToken));
        Task<bool> SaveEntitiesAsync(CancellationToken cancellation = default(CancellationToken));
    }
}
