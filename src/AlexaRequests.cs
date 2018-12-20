using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Alexa.NET.Request;
using Alexa.NET.Response;
using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization.Json;

[assembly: LambdaSerializer(typeof(RokuDotNet.Alexa.Lambda.RokuDirectiveSerializer))]

namespace RokuDotNet.Alexa.Lambda
{
    public static class AlexaRequests
    {
        private delegate Task<DirectiveResponse> AlexaRequest(DirectiveRequest request, ILambdaContext context);

        private static readonly IReadOnlyDictionary<Tuple<string, string>, AlexaRequest> handlers =
            new Dictionary<Tuple<string, string>, AlexaRequest>
            {
                { Tuple.Create("Alexa.Discovery", "Discover"), OnDiscoveryDirectiveAsync }
            };

        public static Task<DirectiveResponse> TestRequestAsync(DirectiveRequest request, ILambdaContext context)
        {
            var directiveId = Tuple.Create(
                request?.Directive?.Header?.Namespace,
                request?.Directive?.Header?.Name);

            if (handlers.TryGetValue(directiveId, out AlexaRequest handler))
            {
                return handler(request, context);
            }

            throw new InvalidOperationException("No handler was registered for this directive.");
        }

        private static Task<DirectiveResponse> OnDiscoveryDirectiveAsync(DirectiveRequest request, ILambdaContext context)
        {
            return Task.FromResult(new DirectiveResponse
            {
                Event = new DiscoveryDirectiveEvent(request.Directive.Header.MessageId)
                {
                    Payload = new DiscoveryDirectiveEventPayload
                    {
                        Endpoints = new[]
                        {
                            new DiscoveryEndpoint
                            {
                                Capabilities = new DiscoveryEndpointCapability[]
                                {
                                    new DiscoveryEndpointCapability
                                    {
                                        Interface = "Alexa.PowerController",
                                        Properties = new DiscoveryDirectiveCapabilityProperties
                                        {
                                            ProactivelySupported = true,
                                            Retrievable = true,
                                            Supported = new[]
                                            {
                                                new DiscoveryDirectiveCapabilityPropertiesSupportedProperty
                                                {
                                                    Name = "powerState"
                                                }
                                            }
                                        },
                                        Type = "AlexaInterface",
                                        Version = "3"
                                    }
                                },
                                Description = "Roku TV by TCL",
                                DisplayCategories = new[] { "TV" },
                                EndpointId = "phoff-device1",
                                FriendlyName = "Living Room TV",
                                ManufacturerName = "TCL"
                            }
                        }
                    }
                }
            });
        }
    }
}
