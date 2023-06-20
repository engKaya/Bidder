using Bidder.UserService.Infastructure.Context;
using Microsoft.EntityFrameworkCore;
using Polly;
using System.Reflection;

namespace Bidder.UserService.Api.Extensions
{
    public static class AddDbContext
    {
        public static IServiceCollection AddContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<UserDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("UserConnectionString"), sqlServerOptionsAction: sqlOptions => { 
                    sqlOptions.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);  
                    sqlOptions.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(5), errorNumbersToAdd: null);
                });
            }); 
            return services;
        }
    }
}
