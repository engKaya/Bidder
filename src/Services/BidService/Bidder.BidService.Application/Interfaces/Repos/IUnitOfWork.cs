namespace Bidder.BidService.Application.Interfaces.Repos
{
    public interface IUnitOfWork : IDisposable
    {
        IBidRepository BidRepository { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellation = default);
    } 
}
