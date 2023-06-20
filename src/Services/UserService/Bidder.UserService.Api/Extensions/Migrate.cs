using Bidder.UserService.Infastructure.Context;

namespace Bidder.UserService.Api.Extensions
{
    public static class Migrate
    {
        public static WebApplication MigrateTables(this WebApplication application) {

            application.MigrationDbContext<UserDbContext>((context, services) =>
            {
                var env = services.GetRequiredService<Microsoft.AspNetCore.Hosting.IHostingEnvironment>();
                var loggger = services.GetRequiredService<ILogger<UserDbContext>>();
                loggger.LogInformation($"Seeding Starting");

                new UserContextSeed().SeedAsync(context,env, loggger).ConfigureAwait(false);
            });


            return application;
        }
    }
}
