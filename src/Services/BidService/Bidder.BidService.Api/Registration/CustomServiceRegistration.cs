using Bidder.BidService.Application.Interfaces.Repos;
using Bidder.BidService.Infastructure.Context;
using Bidder.BidService.Infastructure.Repos;
using Bidder.BidService.Infastructure.Uof;
using Bidder.Domain.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Bidder.BidService.Api.Registration
{
    public static class CustomServiceRegistration
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBidRepository, BidRepository>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddDbContext<BidDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("BidConnectionString"), sqlOptions =>
                { 
                    sqlOptions.MigrationsAssembly(typeof(Program).GetTypeInfo().Assembly.GetName().Name);
                    sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                });
                options.EnableSensitiveDataLogging();
            });
            services.AddMediatR();
            var optionsBuilder = new DbContextOptionsBuilder<BidDbContext>()
                                                .UseSqlServer(configuration.GetConnectionString("BidConnectionString"));
            services.AddLogging(conf => conf.AddConsole()).Configure<LoggerFilterOptions>(cfg => cfg.MinLevel = LogLevel.Debug);


            return services;
        }
        public static void AddMediatR(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(new Assembly[] { Assembly.GetExecutingAssembly() })); 
        }
    }
}
