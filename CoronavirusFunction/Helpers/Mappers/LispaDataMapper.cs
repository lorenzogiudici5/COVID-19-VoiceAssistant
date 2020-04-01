using CoronavirusFunction.Models;
using System;
using System.Globalization;

namespace CoronavirusFunction.Helpers
{
    public static class LispaDataCityMapper
    {
        public static LocationData ToLocationData(this LispaDataCity lispaData)
        {
            if (lispaData == null)
                return null;

            var cityName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lispaData.NomeCom.ToLower());

            return new CityData(cityName)
            {
                CodIstatn = lispaData.CodIstatn,
                Fid = lispaData.Fid,
                Cases = lispaData.Totale,
                Active = lispaData.Positivi,
                Deaths = lispaData.Deceduti,
                Date = DateTimeOffset.Now.Date
            };
        }
    }
}
