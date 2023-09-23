using Bidder.Application.Common.Interfaces;

namespace Bidder.BidService.Application.Interfaces.Repos
{
    public interface IUnitOfWork : IBaseUnitOfWork
    { 
        IBidRepository BidRepository { get; } 
    } 
}
