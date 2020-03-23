using System;
using System.Collections.Generic;
using System.Text;

namespace CoronavirusFunction.Models
{
    /// <summary>
    /// A class level custom attribute that can be used to mark handler classes
    /// with Dialogflow intent name.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class IntentAttribute : Attribute
    {
        public string Name { get; private set; }

        public IntentAttribute(string name)
        {
            Name = name;
        }
    }
}
