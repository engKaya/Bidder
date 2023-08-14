using AutoMapper;
using Bidder.Notification.EventIntegration.AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bidder.Notification.EventIntegration.Extensions
{
    public static class AutoMapperConfigure
    {
        public static void AddAutoMapperCustom (this IServiceCollection services, IConfiguration conf)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MailMappingProfile(conf));
            });
            var mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
