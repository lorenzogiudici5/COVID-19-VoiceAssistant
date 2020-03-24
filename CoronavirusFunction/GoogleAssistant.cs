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

namespace CoronavirusFunction
{
    public static class GoogleAssistant
    {
        private static readonly JsonParser jsonParser = new JsonParser(JsonParser.Settings.Default.WithIgnoreUnknownFields(true));

        [FunctionName("GoogleAssistant")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            try
            {
                log.LogInformation("C# HTTP trigger function processed a request.");

                var dialogflowResponse = new WebhookResponse();
                WebhookRequest dialogflowRequest;
                using (var reader = new StreamReader(req.Body))
                {
                    dialogflowRequest = jsonParser.Parse<WebhookRequest>(reader);
                }

                var user = dialogflowRequest.OriginalDetectIntentRequest.Payload.Fields.ContainsKey("user") ? dialogflowRequest.OriginalDetectIntentRequest.Payload.Fields["user"] : null;
                var userId = user != null && user.StructValue.Fields.ContainsKey("userId") ? user.StructValue.Fields["userId"].StringValue : default(string);

                var sessionId = dialogflowRequest.Session;
                
                var conversation = new Conversation()
                {
                    User = new User(userId),
                    ConversationId = sessionId,
                    Source = Source.Dialogflow
                };

                dialogflowResponse = await conversation.Handle(dialogflowRequest);

                return new OkObjectResult(dialogflowResponse);
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message);
                return new BadRequestResult();
            }
        }
    }
}
