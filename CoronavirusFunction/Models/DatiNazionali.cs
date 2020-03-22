using Newtonsoft.Json;

namespace CoronavirusFunction.Models
{
    public partial class DatiNazionali : Dati
    {
        public override string Name => this.Stato == "ITA" ? "Italia" : Stato;
    }
}
