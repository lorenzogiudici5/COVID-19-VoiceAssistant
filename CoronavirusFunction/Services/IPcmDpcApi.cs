using CoronavirusFunction.Models;
using Newtonsoft.Json;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoronavirusFunction.Services
{
    public interface IPcmDpcApi
    {
        private const string URL_NAZIONE_LATEST = "/dpc-covid19-ita-andamento-nazionale-latest.json";
        private const string URL_NAZIONE = "/dpc-covid19-ita-andamento-nazionale.json";
        private const string URL_REGIONE_LATEST = "/dpc-covid19-ita-regioni-latest.json";
        private const string URL_REGIONE = "/dpc-covid19-ita-regioni.json";
        private const string URL_PROVINCIA_LATEST = "/dpc-covid19-ita-province-latest.json";
        private const string URL_PROVINCIA = "/dpc-covid19-ita-regioni.json";

        [Get("/dpc-covid19-ita-andamento-nazionale.json")]
        Task<IEnumerable<DatiNazionali>> GetDatiNazionali();
        [Get("/dpc-covid19-ita-regioni.json")]
        Task<IEnumerable<DatiRegionali>> GetDatiRegionali();
        [Get("/dpc-covid19-ita-province.json")]
        Task<IEnumerable<DatiProvinciali>> GetDatiProvinciali();

        [Get("/dpc-covid19-ita-andamento-nazionale-latest.json")]
        Task<IEnumerable<DatiNazionali>> GetLastDatiNazionali();
        [Get("/dpc-covid19-ita-regioni-latest.json")]
        Task<IEnumerable<DatiRegionali>> GetLastDatiRegionali();
        [Get("/dpc-covid19-ita-province-latest.json")]
        Task<IEnumerable<DatiProvinciali>> GetLastDatiProvinciali();
    }
}
