using Newtonsoft.Json;

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

        [JsonProperty("updated")]
        public long Updated { get; set; }

        [JsonProperty("active")]
        public long Active { get; set; }
    }
}
