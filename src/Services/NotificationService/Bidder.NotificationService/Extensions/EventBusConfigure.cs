using Bidder.NotificationService.IntegrationEvents.User.EventHandler;
using Bidder.NotificationService.IntegrationEvents.User.Events;
using EventBus.Base;
using EventBus.Base.Abstraction;
using EventBus.Factory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text.Json;

namespace Bidder.NotificationService.Extensions
{
    public static class EventBusConfigure
    { 
        public static void AddEventBus(this IServiceCollection services, IConfiguration conf, ILogger logger)
        {
            try
            {
                var _uri = conf["RabbitSettings:uri"];
                var _hostname = conf["RabbitSettings:host"];
                var _port = int.Parse(conf["RabbitSettings:port"]);
                var _username = conf["RabbitSettings:username"];
                var _password = conf["RabbitSettings:password"]; 
                services.AddSingleton<IEventBus>(sp =>
                {
                    logger.LogInformation("Event Bus Service Creation Has Started");
                    EventBusConfig config = new()
                    {
                        ConnectionRetry = 5,
                        SubscriberClientAppName = "Bidder.NotificationService",
                        DefaultTopicName = "Bidder",
                        EventBusType = EventBusType.RabbitMQ,
                        EventNameSuffix = "IntegrationEvent",
                        Connection = new ConnectionFactory()
                        {
                            HostName = _hostname,
                            Port = _port,
                            UserName = _username,
                            Password = _password,
                            Uri = new Uri(_uri)
                        }
                    };
                    var ebus = EventBusFactory.CreateEventBus(config, sp);
                    logger.LogInformation($"Event Bus Service Creation Has Finished");
                    return ebus;
                });

                IEventBus eventBus = services.BuildServiceProvider().GetRequiredService<IEventBus>();
                eventBus.AddSubscriptions();
                services.AddHandlersTransient();
            }
            catch (Exception ex)
            {
                logger.LogCritical("An Error Has Occured At EventBus registration", ex);
            } 
        }

        public static void AddSubscriptions(this IEventBus eventBus)
        {
            eventBus.Subscribe<NewUserIntegrationEvent, NewUserIntegrationEventHandler>();
        }

        public static void AddHandlersTransient(this IServiceCollection services)
        {
            services.AddTransient<NewUserIntegrationEventHandler>();
        }
    }
}
