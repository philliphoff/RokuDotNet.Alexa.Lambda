using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RokuDotNet.Alexa.Lambda
{
    public sealed class DirectiveConverter : JsonConverter
    {
        public static readonly IList<IDirectiveTypeConverter> TypeConverters = new List<IDirectiveTypeConverter>();

        public override bool CanRead => true;

        public override bool CanWrite => false;

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Directive);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var directiveObject = JObject.ReadFrom(reader);

            string directiveNamespace = directiveObject?["header"]?["namespace"]?.Value<string>();
            string directiveName = directiveObject?["header"]?["name"]?.Value<string>();

            if (!String.IsNullOrEmpty(directiveNamespace) && !String.IsNullOrEmpty(directiveName))
            {
                var converter = TypeConverters.FirstOrDefault(c => c.CanConvert(directiveNamespace, directiveName));

                if (converter != null)
                {
                    var directive = converter.Convert(directiveNamespace, directiveName);

                    serializer.Populate(directiveObject.CreateReader(), directive);

                    return directive;
                }

                throw new InvalidOperationException("No type converter has been registered for the directive namespace and name.");
            }

            throw new InvalidOperationException("Directive has no namespace or name in its header.");
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}