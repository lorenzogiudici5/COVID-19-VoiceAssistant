using Newtonsoft.Json;

namespace CoronavirusFunction.Models
{
    public class Location
    {
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
        public string SubadminArea { get; set; }

        [JsonProperty("street-address")]
        public string StreetAddress { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        public LocationDefinition Definition => getLocationDefinition();

        private LocationDefinition getLocationDefinition()
        {
            return
                !string.IsNullOrEmpty(this.Country) ? LocationDefinition.Paese :
                !string.IsNullOrEmpty(this.AdminArea) ? LocationDefinition.Regione :
                !string.IsNullOrEmpty(this.SubadminArea) ? LocationDefinition.Provincia :
                LocationDefinition.Citta;
        }
    }
}
