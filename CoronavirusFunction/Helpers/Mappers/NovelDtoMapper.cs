using CoronavirusFunction.Models;
namespace CoronavirusFunction.Helpers.Mappers
{
    public static class NovelDtoMapper
    {
        public static CountryData ToCountryData(this NovelCountryDto novelCountry, string italianName)
        {
            if (novelCountry == null)
                return null;
            
            return new CountryData(italianName)
            {
                ItalianName = italianName,
                Country = novelCountry.Country,
                FlagUri = novelCountry.CountryInfo.Flag,
                Iso2 = novelCountry.CountryInfo.Iso2,
                Iso3 = novelCountry.CountryInfo.Iso3,
                Lat = novelCountry.CountryInfo.Lat,
                Long = novelCountry.CountryInfo.Long,
                
                Cases = novelCountry.Cases,
                Active = novelCountry.Active,
                Deaths = novelCountry.Deaths,
                Critical = novelCountry.Critical,
                TodayCases = novelCountry.TodayCases,
                Recovered = novelCountry.Recovered,
                Date = novelCountry.Date
            };
        }

        public static WorldData ToWorldData(this NovelWorldDto novelWorld)
        {
            // if culture == it-it -> "Mondo!"
            var name = "Mondo";
            if (novelWorld == null)
                return null;
            
            return new WorldData(name)
            {
                Cases = novelWorld.Cases,
                Active = novelWorld.Active,
                Deaths = novelWorld.Deaths,
                Recovered = novelWorld.Recovered,
                Date = novelWorld.Date
            };
        }
    }
}
