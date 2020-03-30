using System.Globalization;

namespace CoronavirusFunction.Models
{
    public class Location
    {
        public Location (string name, LocationDefinition definition)
        {
            this.Name = name;
            this.Definition = definition;
        }
        public string Name { get; set; }
        public LocationDefinition Definition { get; private set; }
    }
}
