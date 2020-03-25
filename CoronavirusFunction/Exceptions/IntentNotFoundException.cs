namespace CoronavirusFunction.Exceptions
{
    public class IntentNotFoundException : IntentException
    {
        protected const string message = "Non sono riuscito a trovare una risposta alla tua richiesta.";

        public IntentNotFoundException(string intentName) : base(message, intentName) { }
    }
}
