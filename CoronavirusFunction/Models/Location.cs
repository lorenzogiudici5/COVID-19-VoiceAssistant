using Newtonsoft.Json;
using System.Globalization;

namespace CoronavirusFunction.Models
{
    public class Location
    {
        private string description;
        private string subadminArea;

        [JsonProperty("business-name")]
        public string BusinessName { get; set; }

        [JsonProperty("shortcut")]
        public string Shortcut { get; set; }

        [JsonProperty("admin-area")]
        public string AdminArea { get; set; }

        [JsonProperty("island")]
        public string Island { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("zip-code")]
        public string ZipCode { get; set; }

        [JsonProperty("subadmin-area")]
        public string SubadminArea
        {
            get => !string.IsNullOrEmpty(subadminArea) ? subadminArea : (!string.IsNullOrEmpty(this.BusinessName) && !string.IsNullOrEmpty(this.StreetAddress)) ? $"{this.BusinessName} di {this.StreetAddress}" : default(string); 
            set => subadminArea = value; 
        }

        [JsonProperty("street-address")]
        public string StreetAddress { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        public LocationDefinition Definition => getLocationDefinition();

        public string Description { get => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(description.ToLower()); set => description = value; }

        private LocationDefinition getLocationDefinition()
        {
            var definition =
                !string.IsNullOrEmpty(this.Country) ? LocationDefinition.Country :
                !string.IsNullOrEmpty(this.AdminArea) ? LocationDefinition.AdminArea :
                !string.IsNullOrEmpty(this.SubadminArea) || (!string.IsNullOrEmpty(this.BusinessName) && !string.IsNullOrEmpty(this.StreetAddress)) ? LocationDefinition.SubAdminArea :
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
