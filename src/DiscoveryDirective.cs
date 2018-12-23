using Alexa.NET.Request.Type;
using Newtonsoft.Json;

namespace RokuDotNet.Alexa.Lambda
{
    public sealed class DiscoveryDirectivePayload
    {
        [JsonProperty("scope")]
        public DirectiveScope Scope { get; set; }
    }

    public sealed class DiscoveryDirective : Directive
    {
        [JsonProperty("payload")]
        public DiscoveryDirectivePayload Payload { get; set; }
    }
}