using CoronavirusFunction.Models;

namespace CoronavirusFunction.Helpers
{
    public static class LispaDataCityMapper
    {
        public static LocationData ToLocationData(this LispaDataCity lispaData)
        {
            if (lispaData == null)
                return null;

            return new CityData(lispaData.NomeCom)
            {
                CodIstatn = lispaData.CodIstatn,
                Fid = lispaData.Fid,
                Cases = lispaData.Totale,
                Active = lispaData.Positivi,
                Deaths = lispaData.Deceduti
            };
        }
    }
}
