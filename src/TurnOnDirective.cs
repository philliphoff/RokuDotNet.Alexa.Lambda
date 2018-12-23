using Newtonsoft.Json;

namespace RokuDotNet.Alexa.Lambda
{
    public sealed class TurnOnDirective : Directive
    {
        [JsonProperty("endpoint")]
        public DirectiveEndpoint Endpoint { get; set; }
    }
}