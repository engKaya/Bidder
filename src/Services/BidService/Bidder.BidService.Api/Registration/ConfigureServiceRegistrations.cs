using Bidder.Application.Common.Interfaces;
using Bidder.BidService.Application.Interfaces.Repos;
using Bidder.BidService.Application.Interfaces.Services;
using Bidder.BidService.Infastructure.Repos;
using Bidder.BidService.Infastructure.Services;
using Bidder.BidService.Infastructure.Uof;
using Bidder.Domain.Common.Interfaces;
using Bidder.Infastructure.Common.Services;

namespace Bidder.BidService.Api.Registration
{
    public static class ConfigureServiceRegistrations
    {
        public static void AddServiceRegistrations(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCustomRepositories();
            services.AddCustomServices();
            services.AddEventBus(configuration, services.BuildServiceProvider().GetRequiredService<ILogger<Program>>());
        }

        public static void AddCustomRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBidRepository, BidRepository>();
            services.AddScoped<IBidRoomRepository, BidRoomRepository>();
        }

        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IBiddingService, BiddingService>();
        }
    }
}
