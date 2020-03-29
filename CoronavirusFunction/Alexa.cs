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

                #if RELEASE
                if (!await validateRequest(req, skillRequest))
                    return new BadRequestResult();
                #endif

                skillResponse = await Conversation.Handle(skillRequest);                    // Handle Conversation and build response
            }
            catch (Exception ex)
            {
                var reprompt = new Reprompt("Quali dati vuoi sapere?");
                skillResponse =
                    ex is IntentException ?
                    ResponseBuilder.Ask(ex.Message, reprompt) :
                    ResponseBuilder.Ask("C'è stato un imprevisto. Scusa.", reprompt);
                
                telemetryClient.TrackException(ex);
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
            var language = skillRequest.Request.Locale;
            var userId = skillRequest.Session.User?.UserId;
            var user = new Models.User() { UserId = userId, Language = language };
            return new Conversation(sessionId, user, Source.Alexa);
        }
#endregion

#region Private Methods

#endregion
        private static async Task<bool> validateRequest(HttpRequest request, SkillRequest skillRequest)
        {
            request.Headers.TryGetValue("SignatureCertChainUrl", out var signatureChainUrl);
            if (string.IsNullOrWhiteSpace(signatureChainUrl))
            {
                return false;
            }

            Uri certUrl;
            try
            {
                certUrl = new Uri(signatureChainUrl);
            }
            catch
            {
                return false;
            }

            request.Headers.TryGetValue("Signature", out var signature);
            if (string.IsNullOrWhiteSpace(signature))
            {
                return false;
            }

            request.Body.Position = 0;
            var body = await request.ReadAsStringAsync();
            request.Body.Position = 0;

            if (string.IsNullOrWhiteSpace(body))
            {
                return false;
            }

            bool valid = await RequestVerification.Verify(signature, certUrl, body);
            bool isTimestampValid = RequestVerification.RequestTimestampWithinTolerance(skillRequest);

            if (!isTimestampValid)
            {
                valid = false;
            }

            return valid;
        }
    }
}
