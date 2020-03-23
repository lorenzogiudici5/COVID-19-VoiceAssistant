using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using CoronavirusFunction.Services;
using Google.Cloud.Dialogflow.V2;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoronavirusFunction.Models
{
    [Intent("Confirmed")]
    public class ConfirmedIntentHandler : BaseHandler
    {
        //public ConfirmedIntentHandler(Conversation conversation) : base(conversation)
        //{
        //}

        public ConfirmedIntentHandler() : base()
        {
        }

        public override async Task<WebhookResponse> HandleAsync(WebhookRequest request)
        {
            // GET Paramaters
            var parameters = request.QueryResult.Parameters;
            var locationParam = parameters.Fields.ContainsKey("location") && parameters.Fields["location"].ToString().Replace('\"', ' ').Trim().Length > 0 ?
                parameters.Fields["location"].ToString() : //.Replace('\"', ' ').Trim(): 
                string.Empty;

            var dateParam = parameters.Fields.ContainsKey("date") && parameters.Fields["date"].ToString().Replace('\"', ' ').Trim().Length > 0 ?
                parameters.Fields["date"].ToString().Replace('\"', ' ').Trim() :
                string.Empty;

            DateTime? date = null;
            Location location = !string.IsNullOrEmpty(locationParam) ? JsonConvert.DeserializeObject<Location>(locationParam) : new Location() { Country = "Italia" };

            Dati dati = await Covid_Api.GetCoronavirusDati(location, date);

            return new WebhookResponse
            {
                FulfillmentText = dati.TotaleCasi.ToString()
            };
        }

        public override async Task<SkillResponse> HandleAsync(SkillRequest request)
        {
            var intentRequest = request.Request as IntentRequest;
            var country = intentRequest.Intent.Slots["Country"].Value;
            var adminArea = intentRequest.Intent.Slots["AdminArea"].Value;
            var city = intentRequest.Intent.Slots["City"].Value;

            // TODO: GET Paramaters
            var location = new Location() { Country = country, AdminArea = adminArea, City = city };
            DateTime? date = null;

            Dati dati = await Covid_Api.GetCoronavirusDati(location, date);

            return ResponseBuilder.Tell(dati.TotaleCasi.ToString());
        }
    }
}
