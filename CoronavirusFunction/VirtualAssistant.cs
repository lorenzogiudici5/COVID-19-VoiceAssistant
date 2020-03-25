using CoronavirusFunction.Helpers;
using CoronavirusFunction.Models;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using System.Collections.Generic;

namespace CoronavirusFunction
{
    public class VirtualAssistant
    {
        protected TelemetryClient telemetryClient;
        protected Dictionary<string, string> telemetryProperties;

        public VirtualAssistant(TelemetryConfiguration configuration)
        {
            this.telemetryClient = new TelemetryClient(configuration);
            this.telemetryProperties = new Dictionary<string, string>();
        }

        protected virtual void trackConversation(string responseBody)
        {
            telemetryProperties.Add("ResponseBody", responseBody);
            telemetryClient.TrackEvent("Conversation", telemetryProperties);
        }

        protected virtual void trackRequest(Conversation conversation, string requestBody)
        {
            telemetryProperties.Add("ConversationId", conversation.Id);
            telemetryProperties.Add("User", conversation.User.UserId);
            telemetryProperties.Add("IsReturningUser", conversation.User.IsReturningUser.ToString());
            telemetryProperties.Add("Source", conversation.Source.ToDescription());
            telemetryProperties.Add("Intent", conversation.Intent);
            telemetryProperties.Add("RequestBody", requestBody);
        }
    }
}