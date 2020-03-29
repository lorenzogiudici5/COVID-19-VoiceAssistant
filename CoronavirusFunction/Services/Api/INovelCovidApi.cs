using CoronavirusFunction.Models;
using Refit;
using System.Threading.Tasks;

namespace CoronavirusFunction.Services
{
    interface INovelCovidApi
    {
        [Get("/all")]
        Task<NovelWorldDto> GeWorldData();

        [Get("/countries/{countryCode}")]
        Task<NovelCountryDto> GeCountryData(string countryCode);
    }
}
