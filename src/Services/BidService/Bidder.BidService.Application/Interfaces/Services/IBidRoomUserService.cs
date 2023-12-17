using Bidder.BidService.Domain.Entities;

namespace Bidder.BidService.Application.Interfaces.Services
{
    public interface IBidRoomUserService 
    {
        Task<IEnumerable<BidRoomUser>> GetBidRoomUsers(long roomId);
    }
}
