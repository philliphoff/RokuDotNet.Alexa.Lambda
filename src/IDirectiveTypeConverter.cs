namespace RokuDotNet.Alexa.Lambda
{
    public interface IDirectiveTypeConverter
    {
        bool CanConvert(string ns, string name);

        Directive Convert(string ns, string name);
    }
}