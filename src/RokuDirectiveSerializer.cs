namespace RokuDotNet.Alexa.Lambda
{
    public sealed class RokuDirectiveSerializer : DirectiveSerializer
    {
        public RokuDirectiveSerializer()
            : base(
                new IDirectiveTypeConverter[]
                { 
                    new DirectiveTypeConverter<DiscoveryDirective>("Alexa.Discovery", "Discover"),
                    new DirectiveTypeConverter<PowerControllerDirective>("Alexa.PowerController", "TurnOff"),
                    new DirectiveTypeConverter<PowerControllerDirective>("Alexa.PowerController", "TurnOn"),
                    new DirectiveTypeConverter<AdjustVolumeDirective>("Alexa.StepSpeaker", "AdjustVolume")
                })
        {
        }
    }
}