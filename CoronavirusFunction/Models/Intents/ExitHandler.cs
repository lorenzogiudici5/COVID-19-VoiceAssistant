using Alexa.NET.Request;
using Alexa.NET.Response;
using CoronavirusFunction.Services;
using Google.Cloud.Dialogflow.V2;

namespace CoronavirusFunction.Models
{
    [Intent("Stop")]
    public class ExitHandler : BaseTextHandler
    {
        public ExitHandler(Conversation conversation) : base(conversation) { }

        public override string TextToSpeech => "Alla prossima e ricorda: IO RESTO A CASA";

        public override string DisplayText => "#IORESTOACASA";

        public override WebhookResponse Handle(WebhookRequest request) => DialogflowResponse.BuildEndResponse(TextToSpeech, DisplayText);
    }
}
