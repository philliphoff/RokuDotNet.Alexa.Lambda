using System;

namespace RokuDotNet.Alexa.Lambda
{
    public sealed class DirectiveTypeConverter<T> : IDirectiveTypeConverter
        where T : Directive, new()
    {
        private readonly string name;
        private readonly string ns;

        public DirectiveTypeConverter(string ns, string name)
        {
            this.name = name;
            this.ns = ns;
        }

        #region IDirectiveTypeConverter Members

        public bool CanConvert(string ns, string name)
        {
            return StringComparer.OrdinalIgnoreCase.Equals(this.ns, ns)
                && StringComparer.OrdinalIgnoreCase.Equals(this.name, name);
        }

        public Directive Convert(string ns, string name)
        {
            return new T();
        }

        #endregion
    }
}