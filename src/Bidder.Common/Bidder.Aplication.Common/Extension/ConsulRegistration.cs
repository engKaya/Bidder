using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Bidder.Application.Common.Extension
{
    public static class ConsulRegistration
    {
        public static IServiceCollection AddConsulConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(consulConfig =>
            {
                var address = configuration.GetValue<string>("CustomSettings:Consul:Address");
                consulConfig.Address = new Uri(address);
            }));
            return services;
        }

        public static IApplicationBuilder UseConsul(this IApplicationBuilder app)
        {
            var consulClient = app.ApplicationServices.GetRequiredService<IConsulClient>();
            var logger = app.ApplicationServices.GetRequiredService<ILoggerFactory>().CreateLogger("AppExtensions");
            var lifetime = app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();
            var config = app.ApplicationServices.GetRequiredService<IConfiguration>();

            var registration = GenerateAgentServiceRegistration(config); 
            logger.LogInformation("Registering with Consul");
            consulClient.Agent.ServiceDeregister(registration.ID).ConfigureAwait(true);
            consulClient.Agent.ServiceRegister(registration).ConfigureAwait(true);

            lifetime.ApplicationStopping.Register(() =>
            {
                logger.LogInformation("Unregistering from Consul");
                consulClient.Agent.ServiceDeregister(registration.ID).ConfigureAwait(true);
            });

            return app;
        }

        private static AgentServiceRegistration GenerateAgentServiceRegistration(IConfiguration configuration)
        {

            var serviceName = configuration["CustomSettings:Consul:ServiceName"];
            var appHost = configuration["CustomSettings:Consul:AppAddress"];
            if (string.IsNullOrEmpty(appHost))
                throw new ArgumentNullException();

            var uri = new Uri(appHost);

            return new AgentServiceRegistration()
            {
                ID = $"{serviceName}_{uri.Host}:{uri.Port}",
                Name = $"{serviceName}",
                Address = $"{uri.Host}",
                Port = uri.Port,
                Tags = new[] { $"urlprefix-/{serviceName}"},
                Check = new AgentServiceCheck()
                {
                    Interval = TimeSpan.FromSeconds(30),
                    Method = "GET",
                    HTTP = $"{uri.Scheme}://host.docker.internal:{uri.Port}/health/get",
                    Timeout = TimeSpan.FromSeconds(120),
                    TLSSkipVerify = true,
                    Notes = $"Health Check to {uri.Scheme}://{uri.Host}:{uri.Port}/health With Get on every 120 seconds"
                }
            };
        }
    }
}
