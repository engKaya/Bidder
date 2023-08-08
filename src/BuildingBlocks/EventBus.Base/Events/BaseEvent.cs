using EventBus.Base.Abstraction;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace EventBus.Base.Events
{
    public abstract class BaseEvent : IEventBus
    {
        protected readonly IServiceProvider _serviceProvider; 
        protected readonly IEventBusSubscriptionManager subsManager;

        public BaseEvent(IServiceProvider serviceProvider,  EventBusConfig _config)
        {
            _serviceProvider = serviceProvider;  
            config = _config;
            subsManager = new InMemoryEventBusSubscriptionManager(ProcessEventName); ;
        }

        public EventBusConfig config { get; set; }

        public virtual string ProcessEventName(string eventName)
        {
            if (config.DeleteEventPrefix)
            {
                eventName = eventName.Replace(config.EventNamePrefix, "");
            }

            if (config.DeleteEventSuffix)
            {
                eventName = eventName.Replace(config.EventNameSuffix, "");
            }

            return eventName;
        }
        public virtual string GetSubName(string eventName)
        {
            return $"{config.SubscriberClientAppName}.{ProcessEventName(eventName)}";
        }

        public async Task<bool> ProcessEvent(string eventName, string message)
        {
            eventName = ProcessEventName(eventName);
            var processed = false;

            if (subsManager.HasSubscriptionsForEvent(eventName))
            {
                var subscriptions = subsManager.GetHandlersForEvent(eventName);
                using (var scope = _serviceProvider.CreateScope())
                {
                    foreach (var sub in subscriptions)
                    {
                        var handler = _serviceProvider.GetService(sub.HandlerType);
                        if (handler == null) continue;
                        var eventType = subsManager.GetEventTypeByName($"{config.EventNamePrefix}{eventName}{config.EventNameSuffix}");
                        var integrationEvent = JsonConvert.DeserializeObject(message, eventType); 

                        var concreteType = typeof(IIntegrationEventHandler<>).MakeGenericType(eventType);
                        await (Task)concreteType.GetMethod("Handle").Invoke(handler, new object[] { integrationEvent });
                    }
                }
                processed = true;
            }

            return processed;
        }

        public virtual void Dispose()
        {
            config = null;
            subsManager.Clear();
        }
        public abstract void Publish(IntegrationEvent @event);

        public abstract void Subscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>;

        public abstract void Unsubscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>;
    }
}
