using Bidder.IdentityService.Application.Features.Commands.User.CreateUser;
using Bidder.IdentityService.Application.Interfaces.Repos;
using Bidder.IdentityService.Domain.Interfaces;
using Bidder.IdentityService.Infastructure.Context;
using Bidder.IdentityService.Infastructure.Repos;
using Bidder.IdentityService.Infastructure.Uof;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));  
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

            services.AddLogging(conf => conf.AddConsole()).Configure<LoggerFilterOptions>(cfg => cfg.MinLevel = LogLevel.Debug);
            services.ConfigureAuth(configuration);
            return services;
        }

        public static void AddMediatR(this IServiceCollection services)
        { 
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(CreateUserCommand)));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}
