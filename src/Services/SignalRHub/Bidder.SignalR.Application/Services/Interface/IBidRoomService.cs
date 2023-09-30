using Bidder.Domain.Common.BaseClassess;

namespace Bidder.SignalR.Application.Services.Interface
{
    public interface IBidRoomService
    {
        public Task<ResponseMessageNoContent> GetActiveRoomsAndSaveToRedis();
    }
}
