using Newtonsoft.Json;

namespace RokuDotNet.Alexa.Lambda
{
    public sealed class DirectiveScope
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }
    }

}