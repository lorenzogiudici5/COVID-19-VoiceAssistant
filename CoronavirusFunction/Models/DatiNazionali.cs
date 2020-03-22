using Newtonsoft.Json;

namespace CoronavirusFunction.Models
{
    public partial class DatiNazionali : Dati
    {

        [JsonProperty("stato")]
        public string Stato { get; set; }

        public override string Name => this.Stato == "ITA" ? "Italia" : Stato;
    }
}
