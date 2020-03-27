using System;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.ApplicationInsights.Extensibility;
using Google.Protobuf;
using Google.Cloud.Dialogflow.V2;
using CoronavirusFunction.Models;
using CoronavirusFunction.Helpers;
using CoronavirusFunction.Exceptions;
using System.Globalization;

namespace CoronavirusFunction
{
    public class GoogleAssistant : VirtualAssistant
    {
        #region Private Fields
        private readonly JsonParser jsonParser = new JsonParser(JsonParser.Settings.Default.WithIgnoreUnknownFields(true));
        private WebhookResponse dialogflowResponse;
        private WebhookRequest dialogflowRequest;
        private string dialogflowStringResponse;
        #endregion

        #region Ctr
        public GoogleAssistant(TelemetryConfiguration configuration) : base(configuration) { }
        #endregion

        #region Public Methods
        [FunctionName("GoogleAssistant")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req, ILogger log)
        {
            try
            {
                log.LogInformation("Dialogflow HTTP Trigger");


                RequestBody = await req.ReadAsStringAsync();
                Conversation = InitConversation(RequestBody);                               // Build Conversation object

                if (Conversation == null)
                    return new BadRequestResult();

                dialogflowResponse = await Conversation.Handle(dialogflowRequest);          // Handle Conversation and build response
            }
            catch (Exception ex)
            {
                dialogflowResponse = new WebhookResponse();
                dialogflowResponse.FulfillmentText = 
                    ex is IntentException ?
                    ex.Message : 
                    "C'è stato un imprevisto. Scusa, riprova.";
            }
            finally
            {
                dialogflowStringResponse = dialogflowResponse.ToString();                   // We don’t want to use Json.NET — it doesn’t know how to handle Struct
                trackConversation(Conversation, RequestBody, dialogflowStringResponse);     
            }

            return new OkObjectResult(dialogflowStringResponse);       
        }

        public override Conversation InitConversation(string requestBody)
        {
            dialogflowRequest = jsonParser.Parse<WebhookRequest>(RequestBody);

            if (dialogflowRequest == null)
                return null;

            var sessionId = dialogflowRequest.Session.Split('/').LastOrDefault();
            User user = extractUser(dialogflowRequest);
            return new Conversation(sessionId, user, Source.Dialogflow);
        }
        #endregion

        #region Private Methods
        private static User extractUser(WebhookRequest dialogflowRequest)
        {
            var userJson = dialogflowRequest.OriginalDetectIntentRequest.Payload.Fields.ContainsKey("user") ? dialogflowRequest.OriginalDetectIntentRequest.Payload.Fields["user"].ToString() : default(string);
            var userDto = !string.IsNullOrEmpty(userJson) ?
                JsonConvert.DeserializeObject<DialogflowUserDto>(userJson) :
                new DialogflowUserDto();
            return userDto.ToUser();
        }
        #endregion
    }
}
