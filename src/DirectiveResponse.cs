using Newtonsoft.Json;

namespace RokuDotNet.Alexa.Lambda
{
    public sealed class DirectiveContextProperty
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("namespace")]
        public string Namespace { get; set; }

        [JsonProperty("timeOfSample")]
        public string TimeOfSample { get; set; }

        [JsonProperty("uncertaintyInMilliseconds")]
        public double UncertaintyInMilliseconds { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public sealed class DirectiveContext
    {
        [JsonProperty("properties")]
        public DirectiveContextProperty[] Properties { get; set; }
    }

    public abstract class DirectiveEvent
    {
        [JsonProperty("header")]
        public DirectiveHeader Header { get; set; }
    }

    public sealed class DirectiveResponse
    {
        [JsonProperty("context")]
        public DirectiveContext Context { get; set; }

        [JsonProperty("event")]
        public DirectiveEvent Event { get; set; }
    }
}