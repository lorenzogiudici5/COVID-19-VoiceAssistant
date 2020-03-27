using Newtonsoft.Json;

namespace CoronavirusFunction.Models
{
    public class CountryDto
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        public string Name { get; set; }

        [JsonProperty("name")]
        public string ItalianName { get; set; }

        [JsonProperty("alpha2")]
        public string Alpha2 { get; set; }

        [JsonProperty("alpha3")]
        public string Alpha3 { get; set; }
    }
}
