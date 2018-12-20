using Newtonsoft.Json;

namespace RokuDotNet.Alexa.Lambda
{
    public abstract class DirectiveEvent
    {
        [JsonProperty("header")]
        public DirectiveHeader Header { get; set; }
    }

    public sealed class DirectiveResponse
    {
        [JsonProperty("event")]
        public DirectiveEvent Event { get; set; }
    }
}