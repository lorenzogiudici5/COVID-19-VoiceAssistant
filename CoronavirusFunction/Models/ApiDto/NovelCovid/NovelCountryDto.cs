using Newtonsoft.Json;
using System;

namespace CoronavirusFunction.Models
{
    public partial class NovelCountryDto
    {
        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("countryInfo")]
        public CountryInfo CountryInfo { get; set; }

        [JsonProperty("cases")]
        public long Cases { get; set; }
        [JsonProperty("deaths")]
        public long Deaths { get; set; }
        [JsonProperty("recovered")]
        public long Recovered { get; set; }
        [JsonProperty("active")]
        public long Active { get; set; }
        [JsonProperty("critical")]
        public long Critical { get; set; }

        [JsonProperty("todayCases")]
        public long TodayCases { get; set; }
        [JsonProperty("todayDeaths")]
        public long TodayDeaths { get; set; }
        [JsonProperty("casesPerOneMillion")]
        public long CasesPerOneMillion { get; set; }
        [JsonProperty("deathsPerOneMillion")]
        public long DeathsPerOneMillion { get; set; }
    }

    public partial class CountryInfo
    {
        [JsonProperty("_id")]
        public long Id { get; set; }

        [JsonProperty("lat")]
        public long Lat { get; set; }

        [JsonProperty("long")]
        public long Long { get; set; }

        [JsonProperty("flag")]
        public Uri Flag { get; set; }

        [JsonProperty("iso3")]
        public string Iso3 { get; set; }

        [JsonProperty("iso2")]
        public string Iso2 { get; set; }
    }
}
