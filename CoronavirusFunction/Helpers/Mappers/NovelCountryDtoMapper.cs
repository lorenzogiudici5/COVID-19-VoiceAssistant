using CoronavirusFunction.Models;
namespace CoronavirusFunction.Helpers.Mappers
{
    public static class NovelDtoMapper
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
                NuoviAttualmentePositivi = novelCountry.TodayCases,
                DimessiGuariti = novelCountry.Recovered, 
            };

        }

        public static WorldData ToWorldData(this NovelWorldDto novelWorld)
        {
            // if culture == it-it -> "Mondo!"
            var name = "Mondo";
            return new WorldData(name)
            {
                TotaleCasi = novelWorld.Cases,
                TotaleAttualmentePositivi = novelWorld.Active,
                Deceduti = novelWorld.Deaths,
                DimessiGuariti = novelWorld.Recovered,
            };

        }
    }
}
