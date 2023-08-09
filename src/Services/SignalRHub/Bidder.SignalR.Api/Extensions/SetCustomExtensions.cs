namespace Bidder.SignalR.Api.Extensions
{
    public static class SetCustomExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddSignalR();
            return services;
        }
    }
}
