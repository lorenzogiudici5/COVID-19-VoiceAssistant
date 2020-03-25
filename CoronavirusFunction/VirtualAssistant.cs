using System.Collections.Generic;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using CoronavirusFunction.Helpers;
using CoronavirusFunction.Models;
using System.Threading.Tasks;

namespace CoronavirusFunction
{
    public abstract class VirtualAssistant
    {
        protected TelemetryClient telemetryClient;
        protected Dictionary<string, string> telemetryProperties;

        public Conversation Conversation { get; set; }
        public string RequestBody { get; set; }

        public VirtualAssistant(TelemetryConfiguration configuration)
        {
            this.telemetryClient = new TelemetryClient(configuration);
            this.telemetryProperties = new Dictionary<string, string>();
        }

        public abstract Conversation InitConversation(string requestBody);

        protected virtual void trackConversation(Conversation conversation, string requestBody, string responseBody)
        {
            telemetryProperties.Add("ConversationId", conversation.Id);
            telemetryProperties.Add("User", conversation.User.UserId);
            telemetryProperties.Add("IsReturningUser", conversation.User.IsReturningUser.ToString());
            telemetryProperties.Add("Source", conversation.Source.ToDescription());
            telemetryProperties.Add("Intent", conversation.Intent);
            telemetryProperties.Add("RequestBody", requestBody);
            telemetryProperties.Add("ResponseBody", responseBody);
            telemetryClient.TrackEvent("Conversation", telemetryProperties);
        }
    }
}