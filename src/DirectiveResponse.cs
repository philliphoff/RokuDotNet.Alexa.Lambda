using Newtonsoft.Json;

namespace RokuDotNet.Alexa.Lambda
{
    public abstract class DirectiveEvent
    {
        [JsonProperty("header")]
        public DirectiveHeader Header { get; set; }
    }

    public sealed class DirectiveResponse<T>
        where T : DirectiveEvent
    {
        [JsonProperty("event")]
        public T Event { get; set; }
    }
}