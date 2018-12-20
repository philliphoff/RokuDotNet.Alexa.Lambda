using System.Collections.Generic;
using Newtonsoft.Json;

namespace RokuDotNet.Alexa.Lambda
{
    public sealed class DiscoveryEndpointCapability
    {
        [JsonProperty("interface")]
        public string Interface { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }
    }

    public sealed class DiscoveryEndpoint
    {
        [JsonProperty("capabilities")]
        public DiscoveryEndpointCapability Capabilities { get; set; }

        [JsonProperty("cookie")]
        public IDictionary<string, string> Cookie { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("displayCategories")]
        public string[] DisplayCategories { get; set; }

        [JsonProperty("endpointId")]
        public string EndpointId { get; set; }

        [JsonProperty("friendlyName")]
        public string FriendlyName { get; set; }

        [JsonProperty("manufacturerName")]
        public string ManufacturerName { get; set; }
    }

    public sealed class DiscoveryDirectiveEventPayload
    {
        [JsonProperty("endpoints")]
        public DiscoveryEndpoint[] Endpoints { get; set; }
    }

    public sealed class DiscoveryDirectiveEvent : DirectiveEvent
    {
        public DiscoveryDirectiveEvent()
        {
            this.Header =
                new DirectiveHeader
                {
                    Name = "Discover.Response",
                    Namespace = "Alexa.Discovery",
                    PayloadVersion = "3"
                };
        }

        [JsonProperty]
        public DiscoveryDirectiveEventPayload Payload { get; set; }
    }
}