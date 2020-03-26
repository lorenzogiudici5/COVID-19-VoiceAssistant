using Alexa.NET.Request;
using Alexa.NET.Response;
using CoronavirusFunction.Services;
using Google.Cloud.Dialogflow.V2;
using System;

namespace CoronavirusFunction.Models
{
    [Intent("Help")]
    public class HelpHandler : BaseTextHandler
    {
        private const string chips1 = "Situazione in Italia";
        private const string chips2 = "Positivi in Lombardia";
        private const string chips3 = "Decessi a Clusone";
        private const string chips4 = "Decessi in Provincia di Bergamo";                        // chips must be max 25 chars long

        public override string TextToSpeech =>
            $"{DisplayText} {Environment.NewLine}" + 
            $"Prova a dire com'è la {chips1} oppure " +
            $"qual'è il numero dei {chips4}";

        public override string DisplayText =>
            "Al momento posso dirti il totale dei casi confermati, i positivi e i decessi dell'Italia o delle sue Regioni e Province.";

        public HelpHandler(Conversation conversation) : base(conversation) 
        {
            Chips = new string[] { chips1, chips2, chips3, chips4 };
        }
    }
}
