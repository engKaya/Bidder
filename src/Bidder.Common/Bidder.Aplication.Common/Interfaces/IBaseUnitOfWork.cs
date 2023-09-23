namespace Bidder.Application.Common.Interfaces
{
    public interface IBaseUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync(CancellationToken cancellation = default);
    }
}
