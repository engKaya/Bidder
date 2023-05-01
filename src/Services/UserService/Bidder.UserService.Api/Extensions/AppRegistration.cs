using System.Reflection;

namespace Bidder.UserService.Api.Extensions
{
    public static class AppRegistration
    {
        public static void CustomRegistration(this IServiceCollection services)
        {
            
            services.AddMediatR(opt =>
            {
                opt.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
                opt.Lifetime = ServiceLifetime.Transient;
            });
        }
    }
}
