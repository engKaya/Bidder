using EventBus.Base;
using EventBus.Base.Abstraction;
using EventBus.Factory;
using EventBus.UnitTests.Event;
using EventBus.UnitTests.EventHandlers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace EventBus.UnitTests
{
    [TestClass]
    public class EventBusTest
    {
        private ServiceCollection services;
        public EventBusTest()
        {
            services = new ServiceCollection();
            services.AddLogging(conf => conf.AddConsole());
        }
        [TestMethod]
        public void subscribe_test_on_rabbitmq()
        {
            services.AddSingleton<IEventBus>(sp =>
            {
                EventBusConfig conf = new()
                {
                    ConnectionRetry = 5,
                    SubscriberClientAppName = "EventBus.UnitTest",
                    DefaultTopicName = "BidderTest",
                    EventBusType = EventBusType.RabbitMQ,
                    EventNameSuffix = "IntegrationEvent",
                    Connection = new ConnectionFactory()
                    {
                        HostName = "localhost",
                        Port = 5672,
                        UserName = "test",
                        Password = "test",
                        Uri = new Uri("amqp://localhost:5672")
                    }
                };
                return EventBusFactory.CreateEventBus(conf, sp);
            });

            var provider = services.BuildServiceProvider();
            var eventBus = provider.GetRequiredService<IEventBus>(); 
            Assert.IsNotNull(eventBus); 
            eventBus.Subscribe<OrderCreatedIntegrationEvent, OrderCreateIntegrationEventHandler>();
            eventBus.Unsubscribe<OrderCreatedIntegrationEvent, OrderCreateIntegrationEventHandler>();
        }
    }
}