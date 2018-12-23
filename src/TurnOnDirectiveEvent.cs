using Newtonsoft.Json;

namespace RokuDotNet.Alexa.Lambda
{
    public sealed class TurnOnDirectiveEvent : DirectiveEvent
    {
        public TurnOnDirectiveEvent(string messageId, string correlationToken)
        {
            this.Header = new DirectiveHeader
            {
                CorrelationToken = correlationToken,
                MessageId = messageId,
                Name = "Response",
                Namespace = "Alexa",
                PayloadVersion = "3"
            };
        }

        [JsonProperty("endpoint")]
        public DirectiveEndpoint Endpoint { get; set; }
    }
}