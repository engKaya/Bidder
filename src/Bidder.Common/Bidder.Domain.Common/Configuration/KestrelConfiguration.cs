namespace Bidder.Domain.Common.Configuration
{
    public class KestrelConfiguration
    {
        public IReadOnlyDictionary<string, KestreProtocolsConfig> Endpoints { get; set; } = new Dictionary<string, KestreProtocolsConfig>();
    }

    public class KestreProtocolsConfig
    {
        public string Protocols { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
    }
}
