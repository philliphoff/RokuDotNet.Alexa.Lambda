using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Alexa.NET.Request;
using Alexa.NET.Response;
using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization.Json;
using RokuDotNet.Client;
using RokuDotNet.Client.Input;

[assembly: LambdaSerializer(typeof(RokuDotNet.Alexa.Lambda.RokuDirectiveSerializer))]

namespace RokuDotNet.Alexa.Lambda
{
    public static class AlexaRequests
    {
        private delegate Task<DirectiveResponse> AlexaRequest(DirectiveRequest request, ILambdaContext context);

        private static readonly IReadOnlyDictionary<Tuple<string, string>, AlexaRequest> handlers =
            new Dictionary<Tuple<string, string>, AlexaRequest>
            {
                { Tuple.Create("Alexa.Discovery", "Discover"), OnDiscoveryDirectiveAsync },
                { Tuple.Create("Alexa.PowerController", "TurnOff"), OnTurnOffDirectiveAsync },
                { Tuple.Create("Alexa.PowerController", "TurnOn"), OnTurnOnDirectiveAsync },
                { Tuple.Create("Alexa.StepSpeaker", "AdjustVolume"), OnAdjustVolumeDirectiveAsync }
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
                                    },
                                    new DiscoveryEndpointCapability
                                    {
                                        Interface = "Alexa.StepSpeaker",
                                        Properties = new DiscoveryDirectiveCapabilityProperties
                                        {
                                        },
                                        Type = "AlexaInterface",
                                        Version = "3"
                                    }
                                },
                                Description = "Roku TV by TCL",
                                DisplayCategories = new[] { "TV" },
                                EndpointId = "philliphoff-device1",
                                FriendlyName = "Living Room TV",
                                ManufacturerName = "TCL"
                            }
                        }
                    }
                }
            });
        }

        private static Task<DirectiveResponse> OnTurnOnDirectiveAsync(DirectiveRequest request, ILambdaContext context)
        {
            return OnPowerControllerDirectiveAsync(request, context, turnOn: true);
        }

        private static Task<DirectiveResponse> OnTurnOffDirectiveAsync(DirectiveRequest request, ILambdaContext context)
        {
            return OnPowerControllerDirectiveAsync(request, context, turnOn: false);
        }

        private static async Task<DirectiveResponse> OnPowerControllerDirectiveAsync(DirectiveRequest request, ILambdaContext context, bool turnOn)
        {
            var baseUri = new Uri(Environment.GetEnvironmentVariable("ROKU_REST_SERVICE_URI"));
            string deviceKey = Environment.GetEnvironmentVariable("ROKU_REST_SERVICE_DEVICEKEY");

            var powerControllerDirective = (PowerControllerDirective)request.Directive;

            var deviceId = powerControllerDirective?.Endpoint?.EndpointId;

            if (!String.IsNullOrEmpty(deviceId))
            {
                using (var clientHandler = new HttpClientHandler())
                using (var delegatingHandler = new AuthorizationHandler(deviceKey, clientHandler))
                {
                    var device = new HttpRokuDevice(deviceId, new Uri(baseUri, deviceId + "/"), delegatingHandler);

                    await device.Input.KeyPressAsync(turnOn ? SpecialKeys.PowerOn : SpecialKeys.PowerOff);

                    return new DirectiveResponse
                    {
                        Context = new DirectiveContext
                        {
                            Properties = new[]
                            {
                                new DirectiveContextProperty
                                {
                                    Namespace = "Alexa.PowerController",
                                    Name = "powerState",
                                    Value = turnOn ? "ON" : "OFF",
                                    TimeOfSample = DateTimeOffset.Now.ToString("o"),
                                    UncertaintyInMilliseconds = 500
                                }
                            }
                        },
                        Event = new PowerControllerDirectiveEvent(request.Directive.Header.MessageId, request.Directive.Header.CorrelationToken)
                        {
                            Endpoint = powerControllerDirective.Endpoint
                        }
                    };
                }
            }

            throw new InvalidOperationException("No endpoint ID was specified.");
        }

        private static async Task<DirectiveResponse> OnAdjustVolumeDirectiveAsync(DirectiveRequest request, ILambdaContext context)
        {
            var baseUri = new Uri(Environment.GetEnvironmentVariable("ROKU_REST_SERVICE_URI"));
            string deviceKey = Environment.GetEnvironmentVariable("ROKU_REST_SERVICE_DEVICEKEY");

            var adjustVolumeDirective = (AdjustVolumeDirective)request.Directive;

            var deviceId = adjustVolumeDirective?.Endpoint?.EndpointId;

            if (!String.IsNullOrEmpty(deviceId))
            {
                using (var clientHandler = new HttpClientHandler())
                using (var delegatingHandler = new AuthorizationHandler(deviceKey, clientHandler))
                {
                    var device = new HttpRokuDevice(deviceId, new Uri(baseUri, deviceId + "/"), delegatingHandler);

                    await device.Input.KeyPressAsync(adjustVolumeDirective?.Payload?.VolumeSteps > 0 ? SpecialKeys.VolumeUp : SpecialKeys.VolumeDown);

                    return new DirectiveResponse
                    {
                        Context = new DirectiveContext
                        {
                            Properties = new DirectiveContextProperty[]
                            {
                            }
                        },
                        Event = new EndpointResponseEvent(request.Directive.Header.MessageId, request.Directive.Header.CorrelationToken)
                        {
                            Endpoint = adjustVolumeDirective.Endpoint
                        }
                    };
                }
            }

            throw new InvalidOperationException("No endpoint ID was specified.");
        }
    }
}
