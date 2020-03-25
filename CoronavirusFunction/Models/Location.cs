using Newtonsoft.Json;
using System.Globalization;

namespace CoronavirusFunction.Models
{
    public class Location
    {
        private string description;

        public string AdminArea { get; set; }
        public string City { get; set; }
        public string SubadminArea { get; set; }
        public string Country { get; set; }

        public LocationDefinition Definition => getLocationDefinition();

        public string Description { get => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(description.ToLower()); set => description = value; }

        private LocationDefinition getLocationDefinition()
        {
            var definition =
                !string.IsNullOrEmpty(this.AdminArea) ? LocationDefinition.AdminArea :
                !string.IsNullOrEmpty(this.SubadminArea) ? LocationDefinition.SubAdminArea :
                !string.IsNullOrEmpty(this.Country) ? LocationDefinition.Country :
                LocationDefinition.City;

            Description = definition switch
            {
                LocationDefinition.Country => Country,
                LocationDefinition.AdminArea => AdminArea,
                LocationDefinition.SubAdminArea => SubadminArea,
                LocationDefinition.City => City,
                _ => string.Empty,
            };

            return definition;
        }
    }
}
