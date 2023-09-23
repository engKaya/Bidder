using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly; 

namespace Bidder.Application.Common.Extension
{
    public static class MigrateDbContext
    {
        public static void MigrateDatabase<TContext>(this IHost host, Action<TContext, IServiceProvider>? seeder = null) where TContext : DbContext
        { 
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider; 
                var logger = services.GetRequiredService<ILogger<TContext>>(); 
                var context = services.GetRequiredService<TContext>();

                try
                {
                    logger.LogInformation($"Migrating Database associated with context db: {typeof(TContext).Name}");

                    var retry = Policy.Handle<SqlException>()
                            .WaitAndRetry(new TimeSpan[]
                            {
                                TimeSpan.FromSeconds(3),
                                TimeSpan.FromSeconds(5),
                                TimeSpan.FromSeconds(8),
                            });
                    retry.Execute(() => InvokeSeeder(seeder, context, services));

                    logger.LogInformation($"Migrated Database associated with context db: {typeof(TContext).Name}");

                }
                catch (Exception ex)
                {
                    logger.LogError($"An Error Occured While Migrating {typeof(TContext).Name}! Ex: {ex.Message}");
                }
            }
        }
        private static void InvokeSeeder<TContext>(Action<TContext, IServiceProvider>? seeder, TContext context, IServiceProvider service)
            where TContext : DbContext
        {
            context.Database.EnsureCreated();
            context.Database.Migrate();
            if (seeder != null) 
                seeder(context, service);
        }
    }
}
