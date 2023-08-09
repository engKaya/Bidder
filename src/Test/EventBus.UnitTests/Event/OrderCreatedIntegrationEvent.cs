using EventBus.Base.Events;

namespace EventBus.UnitTests.Event
{
    public class OrderCreatedIntegrationEvent : IntegrationEvent
    {
        public OrderCreatedIntegrationEvent(int id)
        {
            this.Id = id;
        }
        public int Id { get; }
    }
}
