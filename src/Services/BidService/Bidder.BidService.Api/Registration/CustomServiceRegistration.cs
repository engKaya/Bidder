using Bidder.BidService.Application.Features.Command.Bidding.CreateBid;
using Bidder.BidService.Application.Mapping;
using Bidder.BidService.Infastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Bidder.BidService.Api.Registration
{
    public static class CustomServiceRegistration
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddServiceRegistrations(configuration);
            services.AddDbContext<BidDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("BidConnectionString"), sqlOptions =>
                { 
                    sqlOptions.MigrationsAssembly(typeof(Program).GetTypeInfo().Assembly.GetName().Name);
                });
                options.EnableSensitiveDataLogging();
            }); 
            services.AddMediatR(); 
            services.AddLogging(conf => conf.AddConsole()).Configure<LoggerFilterOptions>(cfg => cfg.MinLevel = LogLevel.Debug);
            services.AddAutoMapperCustom(configuration);

            return services;
        }
        public static void AddMediatR(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(CreateBidCommand))); 
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}
