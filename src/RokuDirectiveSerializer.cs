namespace RokuDotNet.Alexa.Lambda
{
    public sealed class RokuDirectiveSerializer : DirectiveSerializer
    {
        public RokuDirectiveSerializer()
            : base(
                new[]
                { 
                    new DirectiveTypeConverter<DiscoveryDirective>("Alexa.Discovery", "Discover") 
                })
        {
        }
    }
}