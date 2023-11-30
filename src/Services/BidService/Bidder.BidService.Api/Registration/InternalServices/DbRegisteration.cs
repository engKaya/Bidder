using Bidder.BidService.Infastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Bidder.BidService.Api.Registration.InternalServices
{
    public static class DbRegisteration
    {
        public static void AddAppDbContext(this IServiceCollection services, IConfiguration configuration)
        { 
            services.AddDbContext<BidDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("BidConnectionString"), sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(Program).GetTypeInfo().Assembly.GetName().Name);
                });
                options.EnableSensitiveDataLogging();
            }, contextLifetime: ServiceLifetime.Scoped);
        } 
    }
}
