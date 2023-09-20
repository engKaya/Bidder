using Microsoft.AspNetCore.SignalR;

namespace Bidder.SignalR.Api.Extensions
{
    public static class SetCustomExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
            services.AddSignalR(config =>
            {
                config.MaximumReceiveMessageSize = 128;
                config.EnableDetailedErrors = true;
                config.KeepAliveInterval = TimeSpan.FromMinutes(1);
            });
            return services;
        }
    }
}
