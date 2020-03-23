using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Google.Protobuf;
using Google.Cloud.Dialogflow.V2;
using CoronavirusFunction.Services;
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
                var userId = user != null ? user.StructValue.Fields["userId"].StringValue : default(string);


                //var intentDisplayName = dialogflowRequest.QueryResult.Intent.DisplayName;
                //Intent intent;
                //if (!Enum.TryParse<Intent>(intentDisplayName, out intent))
                //{
                //    dialogflowResponse.FulfillmentText = "Non ho capito cosa mi hai chiesto";
                //    return new OkObjectResult(dialogflowResponse);
                //}

                //// GET Paramaters
                //var parameters = dialogflowRequest.QueryResult.Parameters;
                //var locationParam = parameters.Fields.ContainsKey("location") && parameters.Fields["location"].ToString().Replace('\"', ' ').Trim().Length > 0 ?
                //    parameters.Fields["location"].ToString() : //.Replace('\"', ' ').Trim(): 
                //    string.Empty;

                //var dateParam = parameters.Fields.ContainsKey("date") && parameters.Fields["date"].ToString().Replace('\"', ' ').Trim().Length > 0 ? 
                //    parameters.Fields["date"].ToString().Replace('\"', ' ').Trim() : 
                //    string.Empty;

                //DateTime? date = null;
                //Location location = !string.IsNullOrEmpty(locationParam) ? JsonConvert.DeserializeObject<Location>(locationParam) : new Location() { Country = "Italia" };

                var request = new Request()
                {
                    User = new Models.User(userId),
                    Source = Source.Dialogflow,
                    //Intent = intent,
                    //Location = location,
                    //Date = date,
                };
                dialogflowResponse = await request.Handle(dialogflowRequest);

                // TODO: service
                //Response response = await RequestManager.GetResponse(request);
                //dialogflowResponse.FulfillmentText = response.Text;
                return new OkObjectResult(dialogflowResponse);
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message);
                return new BadRequestResult();
            }
        }
    }

    public enum Intent
    {
        Fallback,
        Confirmed,
        Deaths,
        Positive,
        Welcome,
        Exit
    }

    public enum LocationDefinition
    {
        Paese,
        Regione,
        Provincia,
        Citta
    }
}
