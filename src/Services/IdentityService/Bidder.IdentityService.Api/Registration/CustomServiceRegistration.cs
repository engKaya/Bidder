using Bidder.IdentityService.Application.Interfaces.Repos;
using Bidder.IdentityService.Infastructure.Context;
using Bidder.IdentityService.Infastructure.Repos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Reflection;

namespace Bidder.IdentityService.Api.Registration
{
    public static class CustomServiceRegistration
    {
        public static  IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration configuration)
        { 
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUserRepository, UserRepository>(); 
            services.AddDbContext<UserDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("UserConnectionString")); 
                options.EnableSensitiveDataLogging();
            });
            services.AddMediatR();
            var optionsBuilder = new DbContextOptionsBuilder<UserDbContext>()
                                                .UseSqlServer(configuration.GetConnectionString("UserConnectionString"));

            //using var dbContext = services.BuildServiceProvider().GetService<UserDbContext>();
            //dbContext.Database.EnsureCreated();
            //dbContext.Database.Migrate();
            return services;
        }

        public static void AddMediatR(this IServiceCollection services)
        { 
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}
