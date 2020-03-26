using System.Threading.Tasks;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Google.Cloud.Dialogflow.V2;
using CoronavirusFunction.Helpers;
using CoronavirusFunction.Services;

namespace CoronavirusFunction.Models
{
    [Intent("Positive")]
    public class DataPositiveHandler : BaseDataHandler
    {
        public DataPositiveHandler(Conversation conversation) : base(conversation) { }

        public override async Task<WebhookResponse> HandleAsync(WebhookRequest request)
        {
            DataRequest dataRequest = InitRequestData(request.QueryResult.Parameters.Fields);
            ItalianData data = await Covid_Api.GetCoronavirusDati(dataRequest.Location, dataRequest.Date);

            CardResponse cardResponse = InitCardResponse(dataRequest.Location, data?.ToLongStringPositive(), data?.ToShortStringPositive());
            return cardResponse.ToWebhookResponse();
        }

        public override async Task<SkillResponse> HandleAsync(SkillRequest request)
        {
            var intentRequest = request.Request as IntentRequest;
            DataRequest dataRequest = InitRequestData(intentRequest.Intent.Slots);

            ItalianData data = await Covid_Api.GetCoronavirusDati(dataRequest.Location, dataRequest.Date);

            CardResponse cardResponse = InitCardResponse(dataRequest.Location, data?.ToLongStringPositive(), data?.ToShortStringPositive());
            return cardResponse.ToSkillResponse();
        }
    }
}
