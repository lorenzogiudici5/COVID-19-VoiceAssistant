using System;
using System.IO;
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
using System.Collections.Generic;
using CoronavirusFunction.Services;
using Alexa.NET.Request.Type;
using Alexa.NET;
using System.Linq;

namespace CoronavirusFunction
{
    public static class Alexa
    {
        [FunctionName("Alexa")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string json = await req.ReadAsStringAsync();
            SkillRequest skillRequest = JsonConvert.DeserializeObject<SkillRequest>(json);

            var userId = skillRequest.Session.User?.UserId;

            var location = new Location() { Country = "Italia" };

            var request = new Models.Request()
            {
                User = new Models.User(userId),
                Source = Source.Alexa,
            };

            SkillResponse response = await request.Handle(skillRequest);
            return new OkObjectResult(response);
        }
    }
}
