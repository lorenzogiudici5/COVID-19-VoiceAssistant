﻿using Newtonsoft.Json;

namespace CoronavirusFunction.Models
{
    public class DialogflowLocationDto
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
    }
}
