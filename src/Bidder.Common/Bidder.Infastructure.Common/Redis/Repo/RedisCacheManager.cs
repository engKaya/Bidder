using Bidder.Application.Common.Redis;
using Bidder.Application.Common.Redis.Interface;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text;

namespace Bidder.Infastructure.Common.Redis.Repo
{
    public class RedisCacheManager : IDistributedCacheManager
    {
        private readonly RedisClient _redisClient;

        public RedisCacheManager(RedisClient redisServer)
        {
            this._redisClient = redisServer;
        }

        public T Get<T>(string key)
        {
            var data = Get(key);
            if (data == null)
                return default;
            var utf8String = Encoding.UTF8.GetString(data);
            var result = JsonConvert.DeserializeObject<T>(utf8String);
            return result;
        }

        public byte[] Get(string key)
        {
            return  _redisClient.RedisCache.Get(key);
        }

        public void Set(string key, object value, int? expireTime = null, ExpireTimeUnit timeUnit = ExpireTimeUnit.Minutes)
        {
            var serializedObject = JsonConvert.SerializeObject(value);
            var utf8String = Encoding.UTF8.GetBytes(serializedObject);
            DistributedCacheEntryOptions options = new();
            if (expireTime != null)
            {
                if (timeUnit == ExpireTimeUnit.Minutes)
                    options.SetAbsoluteExpiration(DateTime.Now.AddMinutes(expireTime.Value));
                else if(timeUnit == ExpireTimeUnit.Hours)
                    options.SetAbsoluteExpiration(DateTime.Now.AddHours(expireTime.Value));
                else
                    options.SetAbsoluteExpiration(DateTime.Now.AddDays(expireTime.Value));
            }
            _redisClient.RedisCache.Set(key, utf8String, options);
        }

        public void Refresh(string key)
        {
            _redisClient.RedisCache.Refresh(key);
        }

        public bool Any(string key)
        {
            return _redisClient.RedisCache.Get(key) != null;
        }

        public void Remove(string key)
        {
            _redisClient.RedisCache.Remove(key);
        }
    }
}
