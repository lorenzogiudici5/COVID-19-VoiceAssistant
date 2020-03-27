using CoronavirusFunction.Models;
namespace CoronavirusFunction.Helpers.Mappers
{
    public static class NovelCountryDtoMapper
    {
        public static CountryData ToCountryData(this NovelCountryDto novelCountry, string italianName)
        {
            return new CountryData(italianName)
            {
                ItalianName = italianName,
                Country = novelCountry.Country,
                FlagUri = novelCountry.CountryInfo.Flag,
                Iso2 = novelCountry.CountryInfo.Iso2,
                Iso3 = novelCountry.CountryInfo.Iso3,
                Lat = novelCountry.CountryInfo.Lat,
                Long = novelCountry.CountryInfo.Long,
                
                TotaleCasi = novelCountry.Cases,
                TotaleAttualmentePositivi = novelCountry.Active,
                Deceduti = novelCountry.Deaths,
                TerapiaIntensiva = novelCountry.Critical,
                NuoviAttualmentePositivi = novelCountry.TodayCases
            };

        }
    }
}
