using System;
using System.Threading.Tasks;
using Alexa.NET.Request;
using Alexa.NET.Response;
using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization.Json;

[assembly: LambdaSerializer(typeof(JsonSerializer))]

namespace RokuDotNet.Alexa.Lambda
{
    public static class AlexaRequests
    {
        public static Task<DirectiveResponse<DiscoveryDirectiveEvent>> TestRequestAsync(DirectiveRequest<DiscoveryDirective> request, ILambdaContext context)
        {
            context.Logger.Log("Namespace: " + request?.Directive?.Header?.Namespace ?? "Unkonwn");
            context.Logger.Log("Name: " + request?.Directive?.Header?.Name ?? "Unkonwn");

            return Task.FromResult(new DirectiveResponse<DiscoveryDirectiveEvent>());
        }
    }
}
