using Newtonsoft.Json;

namespace CoronavirusFunction.Models
{
    public partial class DatiRegionali : Dati
    {
        public override string Name => this.DenominazioneRegione;

        [JsonProperty("stato")]
        public string Stato { get; set; }

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
