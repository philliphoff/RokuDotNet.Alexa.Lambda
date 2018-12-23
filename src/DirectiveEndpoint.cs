using Newtonsoft.Json;

namespace RokuDotNet.Alexa.Lambda
{
    public sealed class DirectiveEndpoint
    {
        [JsonProperty("endpointId")]
        public string EndpointId { get; set; }

        [JsonProperty("scope")]
        public DirectiveScope Scope { get; set; }
    }
}