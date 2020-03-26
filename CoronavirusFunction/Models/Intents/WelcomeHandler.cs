using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Response;
using CoronavirusFunction.Services;
using Google.Cloud.Dialogflow.V2;

namespace CoronavirusFunction.Models
{
    [Intent("Welcome")]
    public class WelcomeHandler : BaseHandler
    {
        private const string welcomeText = "Benvenuto! Puoi chiedermi i dati dei positivi, dei deceduti o il totale dei contagiati in Italia e nelle sue Regioni e Province. Cosa vuoi sapere?";
        private const string returningText = "Bentornato! Quali dati vuoi sapere?";

        public WelcomeHandler(Conversation conversation) : base(conversation) { }

        public override WebhookResponse Handle(WebhookRequest request)
        {
            return DialogflowResponse.BuildSimpleResponse(!conversation.User.IsReturningUser ? welcomeText : returningText);
        }

        public override SkillResponse Handle(SkillRequest request)
        {
            return AlexaResponse.BuildSimpleResponse(!conversation.User.IsReturningUser ? welcomeText : returningText);
        }
    }
}
