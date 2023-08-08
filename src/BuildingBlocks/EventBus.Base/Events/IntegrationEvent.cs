using Newtonsoft.Json;

namespace EventBus.Base.Events
{
    public class IntegrationEvent
    {
        public IntegrationEvent()
        {
            Id = Guid.NewGuid();
            CreatedDate = DateTime.UtcNow;
        }
        [System.Text.Json.Serialization.JsonConstructor]
        public IntegrationEvent(Guid id, DateTime createDate)
        {
            Id = id;
            CreatedDate = createDate;
        }
        [JsonProperty]
        public Guid Id { get; private set; }
        [JsonProperty]
        public DateTime CreatedDate { get; private set; }
    }
}
