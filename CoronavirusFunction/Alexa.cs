using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Alexa.NET.Response;
using Alexa.NET.Request;
using CoronavirusFunction.Models;
using Microsoft.ApplicationInsights.Extensibility;
using System;
using CoronavirusFunction.Exceptions;
using Alexa.NET;

namespace CoronavirusFunction
{
    public class Alexa : VirtualAssistant
    {
        public Alexa (TelemetryConfiguration configuration) : base (configuration) { }

        [FunctionName("Alexa")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            try
            {
                log.LogInformation("Alexa HTTP trigger");

                string requestBody = await req.ReadAsStringAsync();
                SkillRequest skillRequest = JsonConvert.DeserializeObject<SkillRequest>(requestBody);

                var sessionId = skillRequest.Session.SessionId;
                var userId = skillRequest.Session.User?.UserId;

                var conversation = new Conversation()
                {
                    User = new Models.User() { UserId = userId },
                    Id = sessionId,
                    Source = Source.Alexa,
                };

                trackRequest(conversation, requestBody);
                SkillResponse alexaResponse = await conversation.Handle(skillRequest);

                trackConversation(JsonConvert.SerializeObject(alexaResponse));

                return new OkObjectResult(alexaResponse);
            }
            catch (Exception ex) when (ex is IntentException)
            {
                telemetryClient.TrackException(ex);
                var reprompt = new Reprompt("Quali dati vuoi sapere?");
                var exceptionResponse = ResponseBuilder.Ask(ex.Message, reprompt);
                return new OkObjectResult(exceptionResponse);
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message);
                return new BadRequestResult();
            }
        }
    }
}
