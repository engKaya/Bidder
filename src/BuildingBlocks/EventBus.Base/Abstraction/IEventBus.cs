using EventBus.Base.Events;

namespace EventBus.Base.Abstraction
{
    public interface IEventBus : IDisposable
    {
        void Publish(IntegrationEvent @event, string topic = null);
        void Subscribe<T, TH>(string topic = null)
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>;
        void Unsubscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>;
    }
}
