using System;

namespace CoronavirusFunction.Exceptions
{
    public class HandlerException : IntentException
    {
        private const string message = "Ho avuto qualche problema ad elaborare la risposta. Vuoi chiedermi altro?";

        public HandlerException(string intentName, Exception innerException) : base(message, intentName, innerException) { }
    }
}
