using Bidder.Application.Common.Extension;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.WebHost
    .UseKestrel()
    .UseContentRoot(Directory.GetCurrentDirectory())
    .ConfigureAppConfiguration((context, config) =>
    {
        config
            .SetBasePath(context.HostingEnvironment.ContentRootPath)
            .AddJsonFile("appsettings.json", true, true)
            .AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json",
                true, true)
            .AddJsonFile("ocelot.json")
            .AddEnvironmentVariables();
    })
    .ConfigureServices(services =>
    {
        services.AddOcelot().AddConsul();
        services.AddElasticWithSerilog(Assembly.GetExecutingAssembly().GetName().Name, builder.Configuration, builder.Environment.EnvironmentName);
    }).ConfigureLogging((hostingContext, logging) =>
    {
        logging.AddConsole();
    }).UseIISIntegration();
builder.Services.AddCors(options =>
    {
        options.AddDefaultPolicy(builder =>
        {
            builder.WithOrigins("*")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
    });
var app = builder.Build();

app.UseHttpsRedirection();
app.UseHttpLogging();
app.UseCors();
app.UseOcelot().GetAwaiter().GetResult();
app.Run();
