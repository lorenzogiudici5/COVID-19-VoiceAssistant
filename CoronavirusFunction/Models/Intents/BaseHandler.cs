using Alexa.NET.Request;
using Alexa.NET.Response;
using Google.Cloud.Dialogflow.V2;
using System.Threading.Tasks;

namespace CoronavirusFunction.Models
{
    /// <summary>
    /// Base class for intent handlers. 
    /// </summary>
    public abstract class BaseHandler
    {
        public BaseHandler()
        {
        }

        /// <summary>
        /// Base async method that simply returns a Task with null. 
        /// Subclasses can override to provide an implementation.
        /// </summary>
        /// <param name="request">Webhook request</param>
        /// <returns>Task with null</returns>

        public virtual Task<WebhookResponse> HandleAsync(WebhookRequest request)
        {
            return Task.FromResult<WebhookResponse>(null);
        }

        /// <summary>
        /// Base method that simply returns null.
        /// Sublasses can override to provide an implementation.
        /// </summary>
        /// <param name="request">Webhook request</param>
        /// <returns>null</returns>
        public virtual WebhookResponse Handle(WebhookRequest request)
        {
            return null;
        }

        /// <summary>
        /// Base async method that simply returns a Task with null. 
        /// Subclasses can override to provide an implementation.
        /// </summary>
        /// <param name="request">SkillRequest request</param>
        /// <returns>Task with null</returns>

        public virtual Task<SkillResponse> HandleAsync(SkillRequest request)
        {
            return Task.FromResult<SkillResponse>(null);
        }

        /// <summary>
        /// Base method that simply returns null.
        /// Sublasses can override to provide an implementation.
        /// </summary>
        /// <param name="request">SkillRequest request</param>
        /// <returns>null</returns>
        public virtual SkillResponse Handle(SkillRequest request)
        {
            return null;
        }
    }
}
