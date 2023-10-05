using Bidder.Application.Common.Interfaces;
using Bidder.Application.Common.Redis;
using Bidder.Application.Common.Redis.Interface;
using Bidder.Infastructure.Common.Extensions;
using Bidder.Infastructure.Common.Redis.Repo;
using Bidder.Infastructure.Common.Services;
using Bidder.SignalR.Application.Redis.Implementation;
using Bidder.SignalR.Application.Redis.Interface;
using Bidder.SignalR.Application.Services.Implementation;
using Bidder.SignalR.Application.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text.Json.Serialization;

namespace Bidder.SignalR.Api.Extensions
{
    public static class SetCustomExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCorsCustom();
            services.AddSignalRCustom();
            services.SetCustomServices();
            services.GetRoomsAndSetRedis();
            services.ConfigureAuth(configuration, new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    var accessToken = context.Request.Query["access_token"];

                    var path = context.HttpContext.Request.Path;
                    var test = path.Value;
                    if (!string.IsNullOrEmpty(accessToken) &&
                         (path.Value.Contains("hub")))
                    {
                        context.Token = accessToken;
                    }
                    return Task.CompletedTask;
                }
            });

            return services;
        }

        private static void SetCustomServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddSingleton<RedisClient>();
            services.AddSingleton<IRoomRedisService, RoomRedisService>();
            services.AddSingleton<IDistributedCacheManager, RedisCacheManager>();
            services.AddSingleton<IBidRoomService, BidRoomService>();
        }

        private static void AddSignalRCustom(this IServiceCollection services)
        {
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
        }

        private static void AddCorsCustom(this IServiceCollection services)
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
        }
    }
}
