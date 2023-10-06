using Bidder.Application.Common.Redis.Interface;
using Bidder.Domain.Common.Dto.BidService.IBiddingService;
using Bidder.SignalR.Application.Redis.Interface;

namespace Bidder.SignalR.Application.Redis.Implementation
{
    public class RoomRedisService : IRoomRedisService
    {
        private readonly IDistributedCacheManager _redis;

        public RoomRedisService(IDistributedCacheManager redis)
        {
            _redis = redis;
            var key = redis.Get("BidRooms");
            if (key == null) 
                redis.Set("BidRooms", new Dictionary<string, ActiveBidRoom>()); 
        }
        public Task<ActiveBidRoom> CreateOrUpdateRoom(ActiveBidRoom room)
        {
            var redisrooms = GetRedisRooms();
            if (redisrooms == null)
                redisrooms = new Dictionary<string, ActiveBidRoom>();

            if (redisrooms is not null && redisrooms.ContainsKey(room.BidId.ToString()))
            {
                redisrooms[room.BidId.ToString()] = room;
                _redis.Set("BidRooms", redisrooms);
                return Task.FromResult(room);
            } 
            redisrooms?.Add(room.BidId.ToString(), room);
            _redis.Set("BidRooms", redisrooms);
            return Task.FromResult(room);
        }

        public Task<bool> DeleteRoom(Guid bidId)
        {
            var bidrooms = GetRedisRooms();

            if (!bidrooms.ContainsKey(bidId.ToString()))
                Task.FromResult(false);

            bidrooms.Remove(bidId.ToString());
            _redis.Set("BidRooms", bidrooms);
            return Task.FromResult(true);
        }

        public Task<ActiveBidRoom?> GetRoom(Guid bidId)
        {
            return Task.FromResult(GetRedisRoom(bidId));
        }

        public Task<IEnumerable<ActiveBidRoom>> GetRooms()
        {
            var redisrooms = GetRedisRooms();
            return Task.FromResult(redisrooms.Values.AsEnumerable());
        } 

        private IDictionary<string, ActiveBidRoom> GetRedisRooms()
        {
            var redisrooms = _redis.Get<IDictionary<string, ActiveBidRoom>>("BidRooms");
            return redisrooms;
        }
        private ActiveBidRoom? GetRedisRoom(Guid BidId)
        { 
                var redisrooms = _redis.Get<IDictionary<string, ActiveBidRoom>>("BidRooms");
                return redisrooms is not null &&  redisrooms.ContainsKey(BidId.ToString()) ? redisrooms[BidId.ToString()]: null;
        }

        public void AddUserToRoom(string BidId, Guid UserId, string connectionId)
        {
            var redisrooms = GetRedisRooms();
            var room = redisrooms[BidId];
            if (room is null)
                throw new Exception("Room Not Found");
            room.Users.Add(UserId, connectionId);
            redisrooms[BidId] = room;
            _redis.Set("BidRooms", redisrooms);
        }

    } 
}
