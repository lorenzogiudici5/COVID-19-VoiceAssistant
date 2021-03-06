﻿using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Response;
using CoronavirusFunction.Services;
using Google.Cloud.Dialogflow.V2;

namespace CoronavirusFunction.Models
{
    [Intent("Welcome")]
    public class WelcomeHandler : BaseTextHandler
    {
        private const string chips1 = "Situazione in Italia";
        private const string chips2 = "Positivi in Lombardia";
        private const string chips3 = "Decessi a Clusone";
        private const string chips4 = "Decessi in Provincia di Bergamo";                        // chips must be max 25 chars long

        public override string TextToSpeech =>
            !conversation.User.IsReturningUser ?
            "Benvenuto! Puoi chiedermi i dati dei positivi, dei deceduti o il totale dei contagiati in Italia e nelle sue Regioni e Province. Cosa vuoi sapere?" :
            "Bentornato! Quali dati vuoi sapere?";

        public override string DisplayText => "";

        public WelcomeHandler(Conversation conversation) : base(conversation)
        {
            Chips = new string[] { chips1, chips2, chips3, chips4 };
        }
    }
}
