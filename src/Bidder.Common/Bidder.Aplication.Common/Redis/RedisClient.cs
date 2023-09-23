using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace Bidder.Application.Common.Redis
{
    public class RedisClient
    {
        public RedisCache RedisCache;

        public RedisClient()
        {
            var redisOptions = new RedisCacheOptions
            {
                ConfigurationOptions = new ConfigurationOptions
                {
                    EndPoints = { { "127.0.0.1", 6379 } }, 
                    AbortOnConnectFail = false,
                    AllowAdmin = true,
                    ConnectTimeout = 1000,
                }
            };

            var opts = Options.Create(redisOptions);
            RedisCache = new RedisCache(opts);
        }
    }
}
