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

        public VirtualAssistant(TelemetryConfiguration configuration)
        {
            this.telemetryClient = new TelemetryClient(configuration);
        }

        protected virtual void trackConversation(Conversation conversation, string requestBody, string responseBody)
        {
            var properties = new Dictionary<string, string>();
            properties.Add("ConversationId", conversation.Id);
            properties.Add("User", conversation.User.UserId);
            properties.Add("IsReturningUser", conversation.User.IsReturningUser.ToString());
            properties.Add("Source", conversation.Source.ToDescription());
            properties.Add("Intent", conversation.Intent);
            properties.Add("RequestBody", requestBody);
            properties.Add("ResponseBody", responseBody);

            telemetryClient.TrackEvent("Conversation", properties);
        }
    }
}