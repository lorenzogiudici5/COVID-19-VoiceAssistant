using CoronavirusFunction.Models;
using Refit;
using System.Threading.Tasks;

namespace CoronavirusFunction.Services
{
    interface INovelCovidApi
    {
        [Get("/countries/{countryCode}")]
        Task<NovelCountryDto> GeCountryData(string countryCode);
    }
}
