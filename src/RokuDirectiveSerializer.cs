namespace RokuDotNet.Alexa.Lambda
{
    public sealed class RokuDirectiveSerializer : DirectiveSerializer
    {
        public RokuDirectiveSerializer()
            : base(
                new IDirectiveTypeConverter[]
                { 
                    new DirectiveTypeConverter<DiscoveryDirective>("Alexa.Discovery", "Discover"),
                    new DirectiveTypeConverter<TurnOnDirective>("Alexa.PowerController", "TurnOn") 
                })
        {
        }
    }
}