using Newtonsoft.Json;

namespace RokuDotNet.Alexa.Lambda
{
    public sealed class DirectiveHeader
    {
        [JsonProperty("messageId")]
        public string MessageId { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("namespace")]
        public string Namespace { get; set; }

        [JsonProperty("payloadVersion")]
        public string PayloadVersion { get; set;}
    }  
}