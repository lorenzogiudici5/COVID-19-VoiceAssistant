using Newtonsoft.Json;

namespace CoronavirusFunction.Models
{
    public partial class PcmDpcAdminAreaDto : PcmDpcDto
    {
        public override string Description => this.DenominazioneRegione;

        [JsonProperty("codice_regione")]
        public long CodiceRegione { get; set; }

        [JsonProperty("denominazione_regione")]
        public string DenominazioneRegione { get; set; }

        [JsonProperty("lat")]
        public double Lat { get; set; }

        [JsonProperty("long")]
        public double Long { get; set; }
    }
}
