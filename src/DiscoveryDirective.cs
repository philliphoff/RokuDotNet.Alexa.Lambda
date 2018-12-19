using Alexa.NET.Request.Type;
using Newtonsoft.Json;

namespace RokuDotNet.Alexa.Lambda
{
    public sealed class DiscoveryDirectiveScope
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }
    }

    public sealed class DiscoveryDirectivePayload
    {
        [JsonProperty("scope")]
        public DiscoveryDirectiveScope Scope { get; set; }
    }

    public sealed class DiscoveryDirective : Directive
    {
        [JsonProperty("payload")]
        public DiscoveryDirectivePayload Payload { get; set; }
    }
}