using EventBus.Base.Abstraction;
using EventBus.UnitTests.Event;

namespace EventBus.UnitTests.EventHandlers
{
    public class OrderCreateIntegrationEventHandler : IIntegrationEventHandler<OrderCreatedIntegrationEvent>
    {
        public Task Handle(OrderCreatedIntegrationEvent @event)
        {
            return Task.CompletedTask;
        }
    }
}
