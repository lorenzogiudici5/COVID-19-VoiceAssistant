using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Google.Protobuf;
using Google.Cloud.Dialogflow.V2;
using CoronavirusFunction.Services;
using CoronavirusFunction.Models;

namespace CoronavirusFunction
{
    public static class GoogleAssistant
    {
        private static readonly JsonParser jsonParser = new JsonParser(JsonParser.Settings.Default.WithIgnoreUnknownFields(true));

        [FunctionName("GoogleAssistant")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            try
            {
                log.LogInformation("C# HTTP trigger function processed a request.");

                var response = new WebhookResponse();
                WebhookRequest request;
                using (var reader = new StreamReader(req.Body))
                {
                    request = jsonParser.Parse<WebhookRequest>(reader);
                }

                var intentDisplayName = request.QueryResult.Intent.DisplayName;
                Intent intent;
                if (!Enum.TryParse<Intent>(intentDisplayName, out intent))
                {
                    response.FulfillmentText = "Non ho capito cosa mi hai chiesto";
                    return new OkObjectResult(response);
                }

                // GET Paramaters
                var parameters = request.QueryResult.Parameters;
                var locationParam = parameters.Fields.ContainsKey("location") && parameters.Fields["location"].ToString().Replace('\"', ' ').Trim().Length > 0 ?
                    parameters.Fields["location"].ToString() : //.Replace('\"', ' ').Trim(): 
                    string.Empty;
            
                var dateParam = parameters.Fields.ContainsKey("date") && parameters.Fields["date"].ToString().Replace('\"', ' ').Trim().Length > 0 ? 
                    parameters.Fields["date"].ToString().Replace('\"', ' ').Trim() : 
                    string.Empty;

                DateTime? date = null;
                Location location = !string.IsNullOrEmpty(locationParam) ? JsonConvert.DeserializeObject<Location>(locationParam) : new Location() { Country = "Italia" };

                var locationDefinition = getLocationDefinition(location);

                Dati dati = null;

                // TODO: date filter
                   switch (locationDefinition)
                {
                    case LocationDefinition.Paese:
                        dati = await Covid_Api.GetCountryData(location.Country, date);
                        break;
                    case LocationDefinition.Regione:
                        dati = await Covid_Api.GetAdminAreaData(location.AdminArea, date);
                        break;
                    case LocationDefinition.Provincia:
                        var provincia = location.SubadminArea.Split(' ')[2];
                        dati = await Covid_Api.GetDistrictArea(provincia, date);
                        break;
                }
                
                switch (intent)
                {
                    case Intent.TotaleContagiati:
                        response.FulfillmentText = dati != null && dati.TotaleCasi.HasValue ? dati.TotaleCasi.ToString() : "Dati non disponibili";
                        break;
                    case Intent.TotaleDeceduti:
                        response.FulfillmentText = dati != null && dati.Deceduti.HasValue ? dati.Deceduti.ToString() : "Dati non disponibili";
                        break;
                    case Intent.AttualmentePositivi:
                        response.FulfillmentText = dati != null && dati.TotaleAttualmentePositivi.HasValue ? dati.TotaleAttualmentePositivi.ToString() : "Dati non disponibili";
                        break;
                }

                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message);
                return new BadRequestResult();
            }
        }

        private static LocationDefinition getLocationDefinition(Location location)
        {
            return
                !string.IsNullOrEmpty(location.Country) ? LocationDefinition.Paese :
                !string.IsNullOrEmpty(location.AdminArea) ? LocationDefinition.Regione :
                !string.IsNullOrEmpty(location.SubadminArea) ? LocationDefinition.Provincia :
                LocationDefinition.Citta;
        }
    }

    public enum Intent
    {
        TotaleContagiati,
        TotaleDeceduti,
        AttualmentePositivi
    }

    public enum LocationDefinition
    {
        Paese,
        Regione,
        Provincia,
        Citta
    }
}
