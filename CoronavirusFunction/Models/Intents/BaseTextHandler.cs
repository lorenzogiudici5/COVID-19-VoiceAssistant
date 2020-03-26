using Alexa.NET.Request;
using Alexa.NET.Response;
using CoronavirusFunction.Services;
using Google.Cloud.Dialogflow.V2;

namespace CoronavirusFunction.Models
{
    public abstract class BaseTextHandler : BaseHandler
    {
        // chips must be max 25 chars long
        public string[] Chips { get; set; }
        public abstract string TextToSpeech { get; }
        public abstract string DisplayText { get; }

        public BaseTextHandler(Conversation conversation) : base(conversation) { }

        public override WebhookResponse Handle(WebhookRequest request)
        {
            var suggestionChipsMsg = DialogflowResponse.BuildSuggestionChipsMsg(Chips);
            var simpleResponseMsg = DialogflowResponse.BuildSimpleResponseMsg(TextToSpeech, DisplayText);
            return DialogflowResponse.BuildRichResponse(simpleResponseMsg, suggestionChipsMsg);
        }

        public override SkillResponse Handle(SkillRequest request)
        {
            return AlexaResponse.BuildSimpleResponse(TextToSpeech);
        }
    }
}
