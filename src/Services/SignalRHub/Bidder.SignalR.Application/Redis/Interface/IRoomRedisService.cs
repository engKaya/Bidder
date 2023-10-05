using Bidder.Domain.Common.Dto.BidService.IBiddingService;

namespace Bidder.SignalR.Application.Redis.Interface
{
    public  interface IRoomRedisService
    {
        Task<ActiveBidRoom> GetRoom(Guid bidId);
        Task<IEnumerable<ActiveBidRoom>> GetRooms();
        Task<ActiveBidRoom> CreateOrUpdateRoom(ActiveBidRoom room); 
        Task<bool> DeleteRoom(Guid bidId);
    }
}
