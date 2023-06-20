using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Polly;

namespace Bidder.UserService.Infastructure.Context
{
    public class UserContextSeed
    {
        public async Task SeedAsync(UserDbContext userContext, IHostingEnvironment environment, ILogger<UserDbContext> logger)
        {
            var policy = Policy.Handle<SqlException>()
                .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                {
                    logger.LogTrace(ex, $"[Bidder.UserService.Infastructure] Exception {ex.GetType().Name} with message ${ex.Message} detected on attempt {time} of {5}");
                });
            var rootPath = environment.ContentRootPath;
            rootPath.Replace("Bidder.UserService.Api", "Bidder.UserService.Infastructure");
            var setupDirPath = Path.Combine(rootPath, "Seeding");

            await policy.ExecuteAsync(async () =>
            {
                userContext.Database.Migrate();
            });
        }
    }
}
