using Bidder.Application.Common.Extension;
using Bidder.Application.Common.Redis;
using Bidder.Application.Common.Redis.Interface;
using Bidder.Infastructure.Common.Redis.Repo;
using Bidder.SignalR.Application.Redis.Implementation;
using Bidder.SignalR.Application.Redis.Interface;
using Bidder.SignalR.Application.Services.Implementation;
using Bidder.SignalR.Application.Services.Interface;
using Bidder.SignalR.Domain.DTO.RedisEntites;
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
            services.AddSingleton<IRoomRedisService, RoomRedisService>();
            services.AddSingleton<IDistributedCacheManager, RedisCacheManager>();
            services.AddSingleton<IBidRoomService, BidRoomService>();
            services.GetRoomsAndSetRedis();

            return services;
        }
    }
}
