using Newtonsoft.Json;
using System;

namespace CoronavirusFunction.Models
{
    public class DatiProvinciali : Dati
    {
        public override string Name => this.DenominazioneProvincia;

        [JsonProperty("data")]
        public DateTimeOffset Data { get; set; }

        [JsonProperty("stato")]
        public string Stato { get; set; }

        [JsonProperty("codice_regione")]
        public long CodiceRegione { get; set; }

        [JsonProperty("denominazione_regione")]
        public string DenominazioneRegione { get; set; }

        [JsonProperty("codice_provincia")]
        public long CodiceProvincia { get; set; }

        [JsonProperty("denominazione_provincia")]
        public string DenominazioneProvincia { get; set; }

        [JsonProperty("sigla_provincia")]
        public string SiglaProvincia { get; set; }

        [JsonProperty("lat")]
        public double Lat { get; set; }

        [JsonProperty("long")]
        public double Long { get; set; }
    }
}
