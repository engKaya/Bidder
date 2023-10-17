using Bidder.Application.Common.Extension;
using Bidder.IdentityService.Infastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Bidder.IdentityService.Api.Registration
{
    public static class CustomServiceRegistration
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddConsulConfig(configuration);
            services.AddServiceRegistrations(configuration);
            services.AddLogging(conf => conf.AddConsole()).Configure<LoggerFilterOptions>(cfg => cfg.MinLevel = LogLevel.Debug); 
            services.AddDbContext<UserDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("UserConnectionString"));
                options.EnableSensitiveDataLogging();
            }); 
            services.AddEventBus(configuration, services.BuildServiceProvider().GetRequiredService<ILogger<Program>>());
            return services;
        }
    }
}
