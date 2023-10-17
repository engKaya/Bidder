using Bidder.Application.Common.Extension;
using Bidder.Application.Common.Interfaces;
using Bidder.Domain.Common.Interfaces;
using Bidder.IdentityService.Application.Features.Commands.User.CreateUser;
using Bidder.IdentityService.Application.Interfaces.Repos;
using Bidder.IdentityService.Infastructure.Repos;
using Bidder.IdentityService.Infastructure.Uof;
using Bidder.Infastructure.Common.Extensions;
using System.Configuration;
using System.Reflection;

namespace Bidder.IdentityService.Api.Registration
{
    public static class ConfigureServiceRegistrations
    {
        public static void AddServiceRegistrations(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCustomServices();
            services.AddCustomRepositories(); 
            services.AddMediatR();
            services.ConfigureAuth(configuration);
        }


        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IIdentityService, Bidder.Infastructure.Common.Services.IdentityService>();
        }
        public static void AddCustomRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
        }
        public static void AddMediatR(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(CreateUserCommand)));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}
