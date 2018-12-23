using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace RokuDotNet.Alexa.Lambda
{
    internal sealed class AuthorizationHandler : DelegatingHandler
    {
        private readonly string deviceKey;

        public AuthorizationHandler(string deviceKey, HttpMessageHandler innerHandler)
            : base(innerHandler)
        {
            this.deviceKey = deviceKey;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Device", this.deviceKey);

            return base.SendAsync(request, cancellationToken);
        }
    }
}