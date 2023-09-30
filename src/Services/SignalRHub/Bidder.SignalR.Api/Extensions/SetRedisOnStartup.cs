using Bidder.SignalR.Application.Services.Interface;

namespace Bidder.SignalR.Api.Extensions
{
    public static class SetRedisOnStartup
    { 
        public static void GetRoomsAndSetRedis(this IServiceCollection services)
        { 
            var bidRoomService = services.BuildServiceProvider().GetService<IBidRoomService>();
            bidRoomService.GetActiveRoomsAndSaveToRedis().Wait();
        }
    }
}
