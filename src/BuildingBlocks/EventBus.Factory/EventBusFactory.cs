using EventBus.Base;
using EventBus.Base.Abstraction;

namespace EventBus.Factory
{
    public static class EventBusFactory
    {
        public static IEventBus CreateEventBus(EventBusConfig config, IServiceProvider serviceProvider)
        {
            return config.EventBusType switch
            {
                EventBusType.RabbitMQ => new EventBusRabbitMQ(serviceProvider, config),
                _ => new EventBusRabbitMQ(serviceProvider, config),
            };
        }
    }
}
