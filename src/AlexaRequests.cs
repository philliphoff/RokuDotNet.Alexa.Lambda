using System;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization.Json;

namespace RokuDotNet.Alexa.Lambda
{
    public class AlexaRequest
    {
    }

    public class AlexaResponse
    {
    }

    public static class AlexaRequests
    {
        [LambdaSerializer(typeof(JsonSerializer))]
        public static Task<AlexaResponse> TestRequestAsync(AlexaRequest request)
        {
            return Task.FromResult(new AlexaResponse());
        }
    }
}
