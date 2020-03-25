using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.ApplicationInsights.Extensibility;
using Newtonsoft.Json;
using Alexa.NET;
using Alexa.NET.Response;
using Alexa.NET.Request;
using CoronavirusFunction.Models;
using CoronavirusFunction.Exceptions;

namespace CoronavirusFunction
{
    public class Alexa : VirtualAssistant
    {
        #region Private Fields
        private SkillResponse skillResponse;
        private SkillRequest skillRequest;
        #endregion

        #region Ctr
        public Alexa (TelemetryConfiguration configuration) : base (configuration) { }
        #endregion

        #region Public Methods
        [FunctionName("Alexa")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req, ILogger log)
        {
            try
            {
                log.LogInformation("Alexa HTTP trigger");

                RequestBody = await req.ReadAsStringAsync();
                Conversation = InitConversation(RequestBody);                               // Build Conversation object

                if (Conversation == null)
                    return new BadRequestResult();

                skillResponse = await Conversation.Handle(skillRequest);                    // Handle Conversation and build response
            }
            catch (Exception ex)
            {
                var reprompt = new Reprompt("Quali dati vuoi sapere?");
                skillResponse =
                    ex is IntentException ?
                    ResponseBuilder.Ask(ex.Message, reprompt) :
                    ResponseBuilder.Ask("C'è stato un imprevisto. Scusa.", reprompt);
            }
            finally
            {
                trackConversation(Conversation, RequestBody, JsonConvert.SerializeObject(skillResponse));
            }

            return new OkObjectResult(skillResponse);
        }

        public override Conversation InitConversation(string requestBody)
        {
            skillRequest = JsonConvert.DeserializeObject<SkillRequest>(requestBody);

            if (skillRequest == null)
                return null;

            var sessionId = skillRequest.Session.SessionId;
            var userId = skillRequest.Session.User?.UserId;
            var user = new Models.User() { UserId = userId };
            return new Conversation(sessionId, user, Source.Alexa);
        }
        #endregion
    }
}
