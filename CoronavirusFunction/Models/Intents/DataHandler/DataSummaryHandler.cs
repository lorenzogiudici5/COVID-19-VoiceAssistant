using System.Threading.Tasks;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Google.Cloud.Dialogflow.V2;
using CoronavirusFunction.Helpers;
using CoronavirusFunction.Services;

namespace CoronavirusFunction.Models
{
    [Intent("Summary")]
    public class DataSummaryHandler : BaseDataHandler
    {
        public DataSummaryHandler(Conversation conversation) : base(conversation) { }

        public override async Task<WebhookResponse> HandleAsync(WebhookRequest request)
        {
            DataRequest dataRequest = InitRequestData(request.QueryResult.Parameters.Fields);
            LocationData data = await Covid_Api.GetCoronavirusDati(dataRequest.Location, dataRequest.Date);

            CardResponse cardResponse = InitCardResponse(data, data?.ToSpeechSummary(dataRequest.Location.Definition), data?.ToTextSummary(dataRequest.Location.Definition));
            return cardResponse.ToWebhookResponse();
        }

        public override async Task<SkillResponse> HandleAsync(SkillRequest request)
        {
            var intentRequest = request.Request as IntentRequest;
            DataRequest dataRequest = InitRequestData(intentRequest.Intent.Slots);

            LocationData data = await Covid_Api.GetCoronavirusDati(dataRequest.Location, dataRequest.Date);

            CardResponse cardResponse = InitCardResponse(data, data?.ToSpeechSummary(dataRequest.Location.Definition), data?.ToTextSummary(dataRequest.Location.Definition));
            return cardResponse.ToSkillResponse();
        }
    }
}
