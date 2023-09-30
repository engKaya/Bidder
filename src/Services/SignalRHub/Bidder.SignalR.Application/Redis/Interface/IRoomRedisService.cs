using Bidder.Domain.Common.Dto.BidService.IBiddingService;

namespace Bidder.SignalR.Application.Redis.Interface
{
    public  interface IRoomRedisService
    {
        Task<ActiveBidRooms> GetRoom(Guid bidId);
        Task<IEnumerable<ActiveBidRooms>> GetRooms();
        Task<ActiveBidRooms> CreateOrUpdateRoom(ActiveBidRooms room); 
        Task<bool> DeleteRoom(Guid bidId);
    }
}
