using Bidder.UserService.Infastructure.Context;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Bidder.UserService.Api.Extensions
{
    public static class Migrate
    {
        public static WebApplication MigrateTables(this WebApplication application) {

            application.MigrationDbContext<UserDbContext>((context, services) =>
            {
                var env = services.GetRequiredService<IHostingEnvironment>();
                var loggger = services.GetRequiredService<ILogger<UserDbContext>>();
                loggger.LogInformation($"Seeding Starting");

                new UserContextSeed().SeedAsync(context,env, loggger).ConfigureAwait(false);
            });


            return application;
        }
    }
}
