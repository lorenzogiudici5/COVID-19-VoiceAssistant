using Alexa.NET.Request;
using Alexa.NET.Response;
using Google.Cloud.Dialogflow.V2;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoronavirusFunction.Models
{
    /// <summary>
    /// Base class for intent handlers. 
    /// </summary>
    public abstract class BaseHandler
    {
        //protected readonly Conversation _conversation;

        ///// <summary>
        ///// Initializes class with the conversation.
        ///// </summary>
        ///// <param name="conversation">Conversation</param>
        //public BaseHandler(Conversation conversation)
        //{
        //    _conversation = conversation;
        //}

        public BaseHandler()
        {
        }

        /// <summary>
        /// Base async method that simply returns a Task with null. 
        /// Subclasses can override to provide an implementation.
        /// </summary>
        /// <param name="req">Webhook request</param>
        /// <returns>Task with null</returns>

        public virtual Task<WebhookResponse> HandleAsync(WebhookRequest req)
        {
            return Task.FromResult<WebhookResponse>(null);
        }

        /// <summary>
        /// Base method that simply returns null.
        /// Sublasses can override to provide an implementation.
        /// </summary>
        /// <param name="req">Webhook request</param>
        /// <returns>null</returns>
        public virtual WebhookResponse Handle(WebhookRequest req)
        {
            return null;
        }

        /// <summary>
        /// Base async method that simply returns a Task with null. 
        /// Subclasses can override to provide an implementation.
        /// </summary>
        /// <param name="req">Webhook request</param>
        /// <returns>Task with null</returns>

        public virtual Task<SkillResponse> HandleAsync(SkillRequest req)
        {
            return Task.FromResult<SkillResponse>(null);
        }

        /// <summary>
        /// Base method that simply returns null.
        /// Sublasses can override to provide an implementation.
        /// </summary>
        /// <param name="req">Webhook request</param>
        /// <returns>null</returns>
        public virtual SkillResponse Handle(SkillRequest req)
        {
            return null;
        }
    }
}
