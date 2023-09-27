using Bidder.Application.Common.Extension;
using Bidder.Application.Common.Redis;
using Bidder.Application.Common.Redis.Interface;
using Bidder.Infastructure.Common.Redis.Repo;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using StackExchange.Redis;
using System.Text.Json.Serialization;

namespace Bidder.SignalR.Api.Extensions
{
    public static class SetCustomExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("http://localhost:4200", "https://localhost:4200")
                                .AllowAnyMethod()
                                .AllowAnyHeader()
                                .AllowCredentials();
                });
            });
            services.AddSignalR(config =>
            {
                config.MaximumReceiveMessageSize = 128;
                config.EnableDetailedErrors = true;
                config.KeepAliveInterval = TimeSpan.FromMinutes(1);
            }).AddJsonProtocol(options =>
            {
                options.PayloadSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.PayloadSerializerOptions.PropertyNamingPolicy = null;
            });

            services.AddSingleton<RedisClient>();
            services.AddSingleton<IDistributedCacheManager, RedisCacheManager>();

            return services;
        }
    }
}
