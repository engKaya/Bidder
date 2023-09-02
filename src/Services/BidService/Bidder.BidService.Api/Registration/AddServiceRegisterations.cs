using Bidder.BidService.Application.Interfaces.Repos;
using Bidder.BidService.Infastructure.Repos;
using Bidder.BidService.Infastructure.Uof;
using Bidder.Domain.Common.Interfaces;

namespace Bidder.BidService.Api.Registration
{
    public static class AddServiceRegisterations
    {
        public static void AddServiceRegistrations(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBidRepository, BidRepository>();
            services.AddSingleton<IIdentityService, IdentityService>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddEventBus(configuration, services.BuildServiceProvider().GetRequiredService<ILogger<Program>>());
        }
    }
}
