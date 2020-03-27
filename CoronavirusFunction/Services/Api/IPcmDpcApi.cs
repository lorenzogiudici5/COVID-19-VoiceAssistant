using CoronavirusFunction.Models;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoronavirusFunction.Services
{
    public interface IPcmDpcApi
    {
        [Get("/dpc-covid19-ita-andamento-nazionale.json")]
        Task<IEnumerable<PcmDpcCountryDto>> GetItalianDataCountry();
        [Get("/dpc-covid19-ita-regioni.json")]
        Task<IEnumerable<PcmDpcAdminAreaDto>> GetItalianDataAdminArea();
        [Get("/dpc-covid19-ita-province.json")]
        Task<IEnumerable<PcmDpcSubAdminAreaDto>> GetItalianDataSubAdminArea();

        [Get("/dpc-covid19-ita-andamento-nazionale-latest.json")]
        Task<IEnumerable<PcmDpcCountryDto>> GetLastItalianDataCountry();
        [Get("/dpc-covid19-ita-regioni-latest.json")]
        Task<IEnumerable<PcmDpcAdminAreaDto>> GetLastItalianDataAdminArea();
        [Get("/dpc-covid19-ita-province-latest.json")]
        Task<IEnumerable<PcmDpcSubAdminAreaDto>> GetLastItalianDataSubAdminArea();
    }
}
