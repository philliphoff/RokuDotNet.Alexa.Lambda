using Newtonsoft.Json;

namespace RokuDotNet.Alexa.Lambda
{
    public sealed class PowerControllerDirective : Directive
    {
        [JsonProperty("endpoint")]
        public DirectiveEndpoint Endpoint { get; set; }
    }
}