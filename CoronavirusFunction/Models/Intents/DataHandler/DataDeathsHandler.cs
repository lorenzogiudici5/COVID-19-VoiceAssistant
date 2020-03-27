using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using CoronavirusFunction.Helpers;
using CoronavirusFunction.Services;
using Google.Cloud.Dialogflow.V2;
using System.Threading.Tasks;

namespace CoronavirusFunction.Models
{
    [Intent("Deaths")]
    public class DataDeathsHandler : BaseDataHandler
    {
        public DataDeathsHandler(Conversation conversation) : base(conversation) { }

        public override async Task<WebhookResponse> HandleAsync(WebhookRequest request)
        {
            DataRequest dataRequest = InitRequestData(request.QueryResult.Parameters.Fields);
            LocationData data = await Covid_Api.GetCoronavirusDati(dataRequest.Location, dataRequest.Date);

            CardResponse cardResponse = InitCardResponse(data, data?.ToLongStringDeaths(), data?.ToShortStringDeaths());
            return cardResponse.ToWebhookResponse();
        }

        public override async Task<SkillResponse> HandleAsync(SkillRequest request)
        {
            var intentRequest = request.Request as IntentRequest;
            DataRequest dataRequest = InitRequestData(intentRequest.Intent.Slots);

            LocationData data = await Covid_Api.GetCoronavirusDati(dataRequest.Location, dataRequest.Date);

            CardResponse cardResponse = InitCardResponse(data, data?.ToLongStringDeaths(), data?.ToShortStringDeaths());
            return cardResponse.ToSkillResponse();
        }
    }
}
