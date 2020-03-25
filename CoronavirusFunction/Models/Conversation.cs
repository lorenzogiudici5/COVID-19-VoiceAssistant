using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using CoronavirusFunction.Exceptions;
using Google.Cloud.Dialogflow.V2;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CoronavirusFunction.Models
{
    public class Conversation
    {
        private string intentName;

        public Conversation (string id, User user, Source source)
        {
            this.Id = id;
            this.User = user;
            this.Source = source;
        }

        public string Id { get; set; }
        public User User { get; set; }
        public Source Source { get; set; }
        public string Intent { get => intentName; set => intentName = value; }

        public async Task<WebhookResponse> Handle(WebhookRequest request)
        {
            intentName = request.QueryResult.Intent.DisplayName;
            var handler = findHandler(intentName);

            try
            {
                return
                    handler.Handle(request) ??
                    await handler.HandleAsync(request) ??
                    throw new Exception("Errore nell'elaborare la richiesta");
            }
            catch (Exception ex)
            {
                throw new HandlerException(intentName, ex);
            }
        }

        public async Task<SkillResponse> Handle(SkillRequest request)
        {
            if (request.Request is LaunchRequest)
                intentName = "Welcome";
            else if (request.Request is SessionEndedRequest)
                intentName = "Exit";
            else if (request.Request is IntentRequest)
            {
                var intentRequest = request.Request as IntentRequest;
                intentName = intentRequest.Intent.Name.Contains("AMAZON") ?
                    intentRequest.Intent.Name.Split('.')[1].Replace("Intent", "") :
                    intentRequest.Intent.Name;
            }

            var handler = findHandler(intentName);

            try
            {
                return 
                    handler.Handle(request) ??
                    await handler.HandleAsync(request) ??
                    throw new Exception("Errore nell'elaborare la richiesta");
            }
            catch (Exception ex)
            {
                throw new HandlerException(intentName, ex);
            }
        }

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

            var constructorInfo = type.GetConstructor(new[] { GetType() });
            var instance = (BaseHandler)constructorInfo.Invoke(new[] { this });

            if(instance == null)
                throw new IntentNotFoundException(intentName);

            return instance;
        }
    }
}
