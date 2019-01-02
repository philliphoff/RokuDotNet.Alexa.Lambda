using Newtonsoft.Json;

namespace RokuDotNet.Alexa.Lambda
{
    internal sealed class AdjustVolumeDirectivePayload
    {
        [JsonProperty("volumeSteps")]
        public int VolumeSteps { get; set; }
    }

    internal sealed class AdjustVolumeDirective : Directive
    {
        [JsonProperty("endpoint")]
        public DirectiveEndpoint Endpoint { get; set; }

        [JsonProperty("payload")]
        public AdjustVolumeDirectivePayload Payload { get; set; }
    }
}