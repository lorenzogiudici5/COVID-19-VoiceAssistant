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
using System;

namespace CoronavirusFunction
{
    public static class Alexa
    {
        [FunctionName("Alexa")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            try
            {
                log.LogInformation("C# HTTP trigger function processed a request.");

                string requestBody = await req.ReadAsStringAsync();
                SkillRequest skillRequest = JsonConvert.DeserializeObject<SkillRequest>(requestBody);

                var userId = skillRequest.Session.User?.UserId;
                var sessionId = skillRequest.Session.SessionId;

                var conversation = new Conversation()
                {
                    User = new Models.User(userId),
                    Id = sessionId,
                    Source = Source.Alexa,
                };

                SkillResponse alexaResponse = await conversation.Handle(skillRequest);


                return new OkObjectResult(alexaResponse);
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message);
                return new BadRequestResult();
            }
        }
    }
}
