using Alexa.NET;
using Alexa.NET.Response;
using CoronavirusFunction.Models;

namespace CoronavirusFunction.Services
{
    public static class AlexaResponse
    {
        public static SkillResponse BuildSimpleResponse(string textToSpeech, string repromptText = null)
        {
            var reprompt = new Reprompt(repromptText ?? "Quali dati vuoi sapere?");
            return ResponseBuilder.Ask(textToSpeech, reprompt);
        }

        public static SkillResponse BuildCardResponse(CardResponse card, string repromptText = null)
        {
            var reprompt = new Reprompt(repromptText ?? "Quali dati vuoi sapere?");
            return ResponseBuilder.AskWithCard(card.TextToSpeech, card.Title, card.Description, reprompt);
        }

        public static SkillResponse BuildEndResponse(string textToSpeech)
        {
            return ResponseBuilder.Tell(textToSpeech);
        }
    }
}
