using System.Collections.Generic;
using Amazon.Lambda.Serialization.Json;

namespace RokuDotNet.Alexa.Lambda
{
    public abstract class DirectiveSerializer : JsonSerializer
    {
        public DirectiveSerializer(IEnumerable<IDirectiveTypeConverter> converters)
        {
            foreach (var converter in converters)
            {
                DirectiveConverter.TypeConverters.Add(converter);
            }
        }
    }
}