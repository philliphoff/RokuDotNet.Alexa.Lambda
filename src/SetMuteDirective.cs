using Newtonsoft.Json;

namespace RokuDotNet.Alexa.Lambda
{
    internal sealed class SetMuteDirectivePayload
    {
        [JsonProperty("mute")]
        public bool Mute { get; set; }
    }

    internal sealed class SetMuteDirective : Directive
    {
        [JsonProperty("endpoint")]
        public DirectiveEndpoint Endpoint { get; set; }

        [JsonProperty("payload")]
        public SetMuteDirectivePayload Payload { get; set; }
    }
}