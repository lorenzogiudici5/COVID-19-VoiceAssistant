using CoronavirusFunction.Models;
using Newtonsoft.Json;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoronavirusFunction.Services
{
    public interface IPcmDpcApi
    {
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
