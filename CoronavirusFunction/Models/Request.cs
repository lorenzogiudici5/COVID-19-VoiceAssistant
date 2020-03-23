using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Google.Cloud.Dialogflow.V2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoronavirusFunction.Models
{
    public class Request
    {
        public User User { get; set; }
        public Source Source { get; set; }
        public Intent Intent { get; set; }
        public Location Location { get; set; }
        public DateTime? Date { get; set; }
        public LocationDefinition LocationDefinition => getLocationDefinition(this.Location);


        public async Task<WebhookResponse> Handle(WebhookRequest request)
        {
            var intentName = request.QueryResult.Intent.DisplayName;
            var handler = FindHandler(intentName);
            if (handler == null)
            {
                return new WebhookResponse
                {
                    FulfillmentText = "Non ho capito cosa mi hai chiesto"
                };
            }

            // Response must be mapped on WebhookResponse
            try
            {
                //using (_tracer.StartSpan(intentName))
                //{
                    // Call the sync handler, if there is one. If not, call the async handler.
                    // Otherwise, it's an error.
                    return handler.Handle(request) ??
                        await handler.HandleAsync(request) ??
                        new WebhookResponse
                        {
                            FulfillmentText = "Error. Handler did not return a valid response."
                        };
                //}
            }
            catch (Exception e) when (request.QueryResult.Intent.DisplayName != "exception.throw")
            {
                //_exceptionLogger.Log(e);
                //var msg = (e as GoogleApiException)?.Error.Message ?? e.Message;
                return new WebhookResponse
                {
                    FulfillmentText = $"Sorry, there's a problem"
                };
            }
        }

        public async Task<SkillResponse> Handle(SkillRequest request)
        {
            if (request.Request is LaunchRequest)
            {
                var reprompt = new Reprompt("Come ti posso aiutare?");
                return ResponseBuilder.Ask("Ciao! Come ti posso aiutare?", reprompt);
            }
            else if (request.Request is IntentRequest)
            {
                var intentRequest = request.Request as IntentRequest;
                var intentName = intentRequest.Intent.Name;

                var handler = FindHandler(intentName);
                if (handler == null)
                {
                    return ResponseBuilder.Tell("Non ho capito");
                }

                // TODO: handler return Generic Response
                // Response must be mapped on WebhookResponse
                try
                {
                    //using (_tracer.StartSpan(intentName))
                    //{
                    // Call the sync handler, if there is one. If not, call the async handler.
                    // Otherwise, it's an error.
                    return handler.Handle(request) ??
                        await handler.HandleAsync(request) ??
                        ResponseBuilder.Tell("Errore");
                    //}
                }
                catch (Exception e) //when (request.QueryResult.Intent.DisplayName != "exception.throw")
                {
                    //_exceptionLogger.Log(e);
                    //var msg = (e as GoogleApiException)?.Error.Message ?? e.Message;
                    return ResponseBuilder.Tell("Errore");
                }
            }
            else
                return ResponseBuilder.Tell("Nessun intent");
        }

        /// <summary>
        /// Given an Intent name, finds a handler matching the intent name attribute.
        /// </summary>
        /// <param name="intentName">Intent name</param>
        /// <returns>Handler or null, if no intent found</returns>
        private BaseHandler FindHandler(string intentName)
        {
            var baseHandlerTypes = typeof(BaseHandler).Assembly.GetTypes()
                .Where(t => t.IsClass && t.IsSubclassOf(typeof(BaseHandler)));

            var typeList = from baseHandlerType in baseHandlerTypes
                           from attribute in baseHandlerType.GetCustomAttributes(typeof(IntentAttribute), true)
                           where ((IntentAttribute)attribute).Name == intentName
                           select baseHandlerType;

            var type = typeList.FirstOrDefault();
            if (type == null) return null;

            //var constructorInfo = type.GetConstructor(new[] { GetType() });
            //var instance = (BaseHandler)constructorInfo.Invoke(new object[] { this });

            var constructorInfo = type.GetConstructor(Type.EmptyTypes);
            var instance = (BaseHandler)constructorInfo.Invoke(null);
            return instance;
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

    public class User
    {
        private string _userId;

        public User(string userId)
        {
            userId = userId;
        }
        public string UserId { get => _userId; set => _userId = value; }
    }

    public enum Source
    {
        Alexa,
        Dialogflow
    }
}
