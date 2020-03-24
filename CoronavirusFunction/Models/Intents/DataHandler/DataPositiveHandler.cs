using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using CoronavirusFunction.Services;
using Google.Cloud.Dialogflow.V2;
using System.Threading.Tasks;

namespace CoronavirusFunction.Models
{
    [Intent("Positive")]
    public class DataPositiveHandler : BaseDataHandler
    {
        public DataPositiveHandler() : base() { }

        public override async Task<WebhookResponse> HandleAsync(WebhookRequest request)
        {
            DataRequest dataRequest = InitRequestData(request.QueryResult.Parameters.Fields);
            ItalianData data = await Covid_Api.GetCoronavirusDati(dataRequest.Location, dataRequest.Date);

            return BuildWebhookResponse(dataRequest.Location, data?.ToLongStringPositive(), data?.ToShortStringPositive());
        }

        public override async Task<SkillResponse> HandleAsync(SkillRequest request)
        {
            var intentRequest = request.Request as IntentRequest;
            DataRequest dataRequest = InitRequestData(intentRequest.Intent.Slots);

            ItalianData data = await Covid_Api.GetCoronavirusDati(dataRequest.Location, dataRequest.Date);

            return BuildAlexaResponse(dataRequest.Location, data?.ToLongStringPositive(), data?.ToShortStringPositive());
        }
    }
}
