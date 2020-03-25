using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Google.Cloud.Dialogflow.V2;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CoronavirusFunction.Models
{
    public class Conversation
    {
        private string intent;

        public string Id { get; set; }
        public User User { get; set; }
        public Source Source { get; set; }
        public string Intent { get => intent; set => intent = value; }

        public async Task<WebhookResponse> Handle(WebhookRequest request)
        {
            intent = request.QueryResult.Intent.DisplayName;
            var handler = findHandler(intent);
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
            catch (Exception ex)
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
                intent = "Welcome";
            else if (request.Request is SessionEndedRequest)
                intent = "Exit";
            else if (request.Request is IntentRequest)
            {
                var intentRequest = request.Request as IntentRequest;
                intent = intentRequest.Intent.Name.Contains("AMAZON") ?
                    intentRequest.Intent.Name.Split('.')[1].Replace("Intent", "") :
                    intentRequest.Intent.Name;
            }
            else
                return ResponseBuilder.Tell("Non ho capito cosa mi hai chiesto");

            var handler = findHandler(intent);
            if (handler == null)
            {
                return ResponseBuilder.Tell("Non ho capito cosa mi hai chiesto");
            }

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
            catch (Exception ex)
            {
                //_exceptionLogger.Log(e);
                //var msg = (e as GoogleApiException)?.Error.Message ?? e.Message;
                return ResponseBuilder.Tell("Errore");
            }
        }

        /// <summary>
        /// Given an Intent name, finds a handler matching the intent name attribute.
        /// </summary>
        /// <param name="intentName">Intent name</param>
        /// <returns>Handler or null, if no intent found</returns>
        private BaseHandler findHandler(string intentName)
        {
            var baseHandlerTypes = typeof(BaseHandler).Assembly.GetTypes()
                .Where(t => t.IsClass && t.IsSubclassOf(typeof(BaseHandler)));

            var typeList = from baseHandlerType in baseHandlerTypes
                           from attribute in baseHandlerType.GetCustomAttributes(typeof(IntentAttribute), true)
                           where ((IntentAttribute)attribute).Name == intentName
                           select baseHandlerType;

            var type = typeList.FirstOrDefault();
            if (type == null) return null;

            var constructorInfo = type.GetConstructor(Type.EmptyTypes);
            var instance = (BaseHandler)constructorInfo.Invoke(null);
            return instance;
        }
    }
}
