using System;

namespace CoronavirusFunction.Exceptions
{
    public abstract class IntentException : Exception
    {
        public IntentException(string message, string intentName) : base(message)
        {
            this.IntentName = intentName;
        }

        public IntentException(string message, string intentName, Exception innerException) : base(message, innerException)
        {
            this.IntentName = intentName;
        }

        public string IntentName { get; set; }
    }
}
