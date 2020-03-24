using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using CoronavirusFunction.Services;
using Google.Cloud.Dialogflow.V2;
using System;
using System.Threading.Tasks;

namespace CoronavirusFunction.Models
{
    [Intent("Summary")]
    public class DataSummaryHandler : BaseDataHandler
    {
        public DataSummaryHandler() : base() { }

        public override async Task<WebhookResponse> HandleAsync(WebhookRequest request)
        {
            DataRequest dataRequest = InitRequestData(request.QueryResult.Parameters.Fields);
            ItalianData data = await Covid_Api.GetCoronavirusDati(dataRequest.Location, dataRequest.Date);

            var speechResponse = BuildSpeechResponse(data, dataRequest.Location);
            return BuildWebhookResponse(speechResponse, dataRequest.Location, data);
        }

        public override async Task<SkillResponse> HandleAsync(SkillRequest request)
        {
            var intentRequest = request.Request as IntentRequest;
            DataRequest dataRequest = InitRequestData(intentRequest.Intent.Slots);

            ItalianData data = await Covid_Api.GetCoronavirusDati(dataRequest.Location, dataRequest.Date);

            var speechResponse = BuildSpeechResponse(data, dataRequest.Location);
            return BuildAlexaResponse(speechResponse, dataRequest.Location, data);
        }

        protected string BuildSpeechResponse(ItalianData data, Location location) => 
            data == null ? 
            "Dati non disponibili" :
            $"In {location.Description} {data.ToSpeechSummary(location.Definition)}";

        protected WebhookResponse BuildWebhookResponse(string message, Location location, ItalianData data)
        {
            return new WebhookResponse
            {
                FulfillmentText = message
            };
        }
        protected SkillResponse BuildAlexaResponse(string message, Location location, ItalianData data)
        {
            var reprompt = new Reprompt("Vuoi sapere altri dati?");
            return data == null ?
                ResponseBuilder.Ask(message, reprompt) :
                ResponseBuilder.AskWithCard(message, $"{location.Description}", data.ToTextSummary(location.Definition), reprompt);
        }
    }
}
