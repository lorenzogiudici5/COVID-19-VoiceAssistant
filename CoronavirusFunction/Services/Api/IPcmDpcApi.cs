using CoronavirusFunction.Models;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoronavirusFunction.Services
{
    public interface IPcmDpcApi
    {
        [Get("/dpc-covid19-ita-andamento-nazionale.json")]
        Task<IEnumerable<ItalianDataCountry>> GetItalianDataCountry();
        [Get("/dpc-covid19-ita-regioni.json")]
        Task<IEnumerable<ItalianDataAdminArea>> GetItalianDataAdminArea();
        [Get("/dpc-covid19-ita-province.json")]
        Task<IEnumerable<ItalianDataSubAdminArea>> GetItalianDataSubAdminArea();

        [Get("/dpc-covid19-ita-andamento-nazionale-latest.json")]
        Task<IEnumerable<ItalianDataCountry>> GetLastItalianDataCountry();
        [Get("/dpc-covid19-ita-regioni-latest.json")]
        Task<IEnumerable<ItalianDataAdminArea>> GetLastItalianDataAdminArea();
        [Get("/dpc-covid19-ita-province-latest.json")]
        Task<IEnumerable<ItalianDataSubAdminArea>> GetLastItalianDataSubAdminArea();
    }
}
