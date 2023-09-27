using Bidder.Domain.Common.BaseClassess;
using Bidder.SignalR.Domain.DTO.RedisEntites;

namespace Bidder.SignalR.Application.Services.Interface
{
    public interface IBidRoomService
    {
        public Task<ResponseMessageNoContent> GetActiveRoomsAndSaveToRedis();
        public Task<ResponseMessage<List<BidRoomRedis>>> GetRoomsFromBidService();
    }
}
