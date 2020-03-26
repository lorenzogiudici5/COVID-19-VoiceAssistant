using Alexa.NET.Response;
using Google.Cloud.Dialogflow.V2;
using CoronavirusFunction.Models;
using CoronavirusFunction.Services;

namespace CoronavirusFunction.Helpers
{
    public static class CardResponseMapper
    {
        public static WebhookResponse ToWebhookResponse(this CardResponse card)
        {
            return !string.IsNullOrEmpty(card.Title) && !string.IsNullOrEmpty(card.Description) ?
                DialogflowResponse.BuildCardResponse(card) :
                DialogflowResponse.BuildSimpleResponse(card.TextToSpeech);
        }

        public static SkillResponse ToSkillResponse(this CardResponse card, string repromptText = null)
        {
            return !string.IsNullOrEmpty(card.Title) && !string.IsNullOrEmpty(card.Description) ?
                AlexaResponse.BuildCardResponse(card, repromptText) :
                AlexaResponse.BuildSimpleResponse(card.TextToSpeech, repromptText);
        }
    }
}
