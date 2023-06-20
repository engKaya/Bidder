using Bidder.UserService.Domain.Models;
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
            rootPath = rootPath.Replace("Bidder.UserService.Api", "Bidder.UserService.Infastructure");
            var setupDirPath = Path.Combine(rootPath, "Seeding");

            await policy.ExecuteAsync(() => ProcessSeeding(userContext, setupDirPath));
        }

        private async Task ProcessSeeding(UserDbContext context, string setupDirPath)
        {
            if (!context.Users.Any())
            {
                await context.Users.AddRangeAsync(GetUsersFromFile(setupDirPath));
                await context.SaveChangesAsync();
            }
        }

        private IEnumerable<User> GetUsersFromFile(string path)
        {
            try
            { 
                string fileName = Path.Combine(path, "Users.txt");
                var str = File.ReadAllText(fileName);
                var fileContent = File.ReadAllLines(fileName)
                        .Select(i => i.Split(','))
                        .Select(i =>
                        {
                            var user = new User(i[0], i[1], i[2], i[3], i[4], i[5], i[6]);
                            return user;
                        });
                return fileContent;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
} 
