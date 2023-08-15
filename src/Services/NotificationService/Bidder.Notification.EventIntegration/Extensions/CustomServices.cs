using Bidder.Notification.Application.Abstraction.EmailBase;
using Bidder.Notification.Application.Abstraction.User;
using Bidder.Notification.Application.Services.EmailBase;
using Bidder.Notification.Application.Services.User;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bidder.Notification.EventIntegration.Extensions
{
    public static class CustomServices
    {
        public static void AddCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapperCustom(configuration);
            services.AddScoped<IEmailService, BasicAuthEmailClient>(); 
            services.AddScoped<IUserRelatedMails, UserRelatedMails>();
            services.AddScoped<IEmailFactory, EmailFactory>();
        }
    }
}
