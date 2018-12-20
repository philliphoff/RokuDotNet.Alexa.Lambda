using Newtonsoft.Json;

namespace RokuDotNet.Alexa.Lambda
{
    public abstract class Directive
    {
        [JsonProperty("header")]
        public DirectiveHeader Header { get; set; }
    }

    public sealed class DirectiveRequest
    {
        [JsonConverter(typeof(DirectiveConverter))]
        [JsonProperty("directive")]
        public Directive Directive { get; set; }
    }
}