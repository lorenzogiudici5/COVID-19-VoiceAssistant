using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Response;
using Google.Cloud.Dialogflow.V2;

namespace CoronavirusFunction.Models
{
    [Intent("Stop")]
    public class ExitHandler : BaseHandler
    {
        private const string exitText = "Alla prossima e ricorda: IO RESTO A CASA";
        public ExitHandler(Conversation conversation) : base(conversation) { }

        public override WebhookResponse Handle(WebhookRequest request)
        {
            return new WebhookResponse()
            {
                FulfillmentText = exitText
            };
        }

        public override SkillResponse Handle(SkillRequest request)
        {
            return ResponseBuilder.Tell(exitText);
        }
    }
}
