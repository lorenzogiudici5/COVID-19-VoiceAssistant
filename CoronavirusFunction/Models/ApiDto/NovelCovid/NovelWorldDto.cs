using Newtonsoft.Json;
using System;

namespace CoronavirusFunction.Models
{
    public partial class NovelWorldDto
    {
        [JsonProperty("cases")]
        public long Cases { get; set; }
        [JsonProperty("deaths")]
        public long Deaths { get; set; }
        [JsonProperty("recovered")]
        public long Recovered { get; set; }
        [JsonProperty("active")]
        public long Active { get; set; }

        [JsonProperty("updated")]
        public long Updated { get; set; }
        public DateTimeOffset Date => DateTimeOffset.FromUnixTimeMilliseconds(Updated);
    }
}
