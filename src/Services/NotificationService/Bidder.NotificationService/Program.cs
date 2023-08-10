using Bidder.NotificationService.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var environmentName = Environment.GetEnvironmentVariable("ENVIRONMENT");
Console.WriteLine($"NotificationService ({environmentName}) Has Started");


IServiceCollection services = new ServiceCollection();


var builder = new ConfigurationBuilder()
            .AddJsonFile($"appsettings.json", true, true)
            .AddJsonFile($"appsettings.{environmentName}.json", true, true)
            .AddEnvironmentVariables();

var config = builder.Build();

services.AddLogging(loggerBuilder =>
{
    loggerBuilder.ClearProviders();
    loggerBuilder.AddConsole();
});


var serviceProvider = services.BuildServiceProvider();

var logger = serviceProvider.GetService<ILogger<Program>>();

services.AddEventBus(config, logger);

Console.ReadLine();