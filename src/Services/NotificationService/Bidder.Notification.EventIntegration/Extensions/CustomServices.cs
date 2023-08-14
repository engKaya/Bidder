using Bidder.Notification.Application.Abstraction;
using Bidder.Notification.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bidder.Notification.EventIntegration.Extensions
{
    public static class CustomServices
    {
        public static void AddCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapperCustom(configuration);
            services.AddSingleton<IEmailService, BasicAuthEmailClient>(); 
        }
    }
}
