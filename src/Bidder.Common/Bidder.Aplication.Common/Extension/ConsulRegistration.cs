using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using IServer = Microsoft.AspNetCore.Hosting.Server.IServer;

namespace Bidder.Application.Common.Extension
{
    public static class ConsulRegistration
    {
        public static IServiceCollection AddConsul(this IServiceCollection services, IConfiguration configuration)
        {
            var consulClient = new ConsulClient(config =>
            {
                var address = configuration["CustomSettings:Consul:Address"];
                if (!string.IsNullOrEmpty(address))
                {
                    config.Address = new Uri(address);
                }
            });
            services.AddSingleton<IConsulClient>(p => consulClient);
            return services;
        }
        public static void RegisterConsul(this IApplicationBuilder app, IServer server, IConfiguration configuration)
        {
            var consulClient = app.ApplicationServices.GetRequiredService<IConsulClient>();
            var lifetime = app.ApplicationServices.GetRequiredService<IApplicationLifetime>();

            var registration = GenerateAgentServiceRegistration(app, server, configuration); 
            consulClient.Agent.ServiceRegister(registration).Wait();
            lifetime.ApplicationStopping.Register(() =>
            {
                consulClient.Agent.ServiceDeregister(registration.ID).Wait();
            });
        }

        private static AgentServiceRegistration GenerateAgentServiceRegistration(IApplicationBuilder app, IServer server, IConfiguration configuration)
        {

            var serviceName = configuration["CustomSettings:Consul:ServiceName"];

            var features = server.Features;
            var addresses = features.Get<IServerAddressesFeature>();
            var address = addresses.Addresses.FirstOrDefault(); // = http://localhost:5000

            var uri = new Uri(address);

            return new AgentServiceRegistration()
            {
                ID = $"{serviceName}_{uri.Host}:{uri.Port}",
                Name = $"{serviceName}",
                Address = $"{uri.Host}",
                Port = uri.Port,
                Tags = new[] { $"urlprefix-/{serviceName}", "Basket", "Redis" },
                Check = new AgentServiceCheck()
                {
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),
                    Interval = TimeSpan.FromSeconds(30),
                    Method = "GET",
                    HTTP = $"{uri.Scheme}://host.docker.internal:{uri.Port}/api/health",
                    Timeout = TimeSpan.FromSeconds(120),
                    TLSSkipVerify = true,
                    Notes = $"Health Check to {uri.Scheme}://{uri.Host}:{uri.Port}/health With Get on every 30 seconds"
                }
            }; 
        }
    }
}
