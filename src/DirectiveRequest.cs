using Newtonsoft.Json;

namespace RokuDotNet.Alexa.Lambda
{
    public abstract class Directive
    {
        [JsonProperty("header")]
        public DirectiveHeader Header { get; set; }
    }

    public sealed class DirectiveRequest<T>
        where T : Directive
    {
        [JsonProperty("directive")]
        public T Directive { get; set; }
    }
}