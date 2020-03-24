using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Response;
using Google.Cloud.Dialogflow.V2;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoronavirusFunction.Models
{
    [Intent("Exit")]
    public class ExitHandler : BaseHandler
    {
        private const string exitText = "Alla prossima e ricordati: IO RESTO A CASA";
        public ExitHandler()
        {
        }

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
