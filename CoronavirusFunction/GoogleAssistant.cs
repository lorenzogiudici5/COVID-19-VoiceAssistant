using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Google.Protobuf;
using Google.Cloud.Dialogflow.V2;
using CoronavirusFunction.Models;
using Microsoft.ApplicationInsights.Extensibility;
using Newtonsoft.Json;
using System.Linq;
using CoronavirusFunction.Helpers;
using CoronavirusFunction.Exceptions;

namespace CoronavirusFunction
{
    public class GoogleAssistant : VirtualAssistant
    {
        private readonly JsonParser jsonParser = new JsonParser(JsonParser.Settings.Default.WithIgnoreUnknownFields(true));

        public GoogleAssistant(TelemetryConfiguration configuration) : base(configuration) { }

        [FunctionName("GoogleAssistant")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            try
            {
                log.LogInformation("Dialogflow HTTP Trigger");

                var dialogflowResponse = new WebhookResponse();
                WebhookRequest dialogflowRequest;
                string requestBody;
                using (var reader = new StreamReader(req.Body))
                {
                    requestBody = reader.ReadToEnd();
                    dialogflowRequest = jsonParser.Parse<WebhookRequest>(requestBody);
                }

                var userJson = dialogflowRequest.OriginalDetectIntentRequest.Payload.Fields.ContainsKey("user") ? dialogflowRequest.OriginalDetectIntentRequest.Payload.Fields["user"].ToString() : default(string);
                var userDto = !string.IsNullOrEmpty(userJson) ? JsonConvert.DeserializeObject<DialogflowUserDto>(userJson) : new DialogflowUserDto();
                                var sessionId = dialogflowRequest.Session.Split('/').LastOrDefault();
                
                var conversation = new Conversation()
                {
                    User = userDto.ToUser(),
                    Id = sessionId,
                    Source = Source.Dialogflow
                };

                trackRequest(conversation, requestBody);
                dialogflowResponse = await conversation.Handle(dialogflowRequest);

                trackConversation(JsonConvert.SerializeObject(dialogflowResponse));

                return new OkObjectResult(dialogflowResponse);
            }
            catch (Exception ex) when (ex is IntentException)
            {
                telemetryClient.TrackException(ex, telemetryProperties);

                var exceptionResponse = new WebhookResponse()
                {
                    FulfillmentText = ex.Message
                };
                return new OkObjectResult(exceptionResponse);
            }
            catch (Exception ex)
            {
                telemetryClient.TrackException(ex, telemetryProperties);
                return new BadRequestResult();
            }
        }
    }
}
