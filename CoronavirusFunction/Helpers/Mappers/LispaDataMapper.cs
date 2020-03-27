using CoronavirusFunction.Models;

namespace CoronavirusFunction.Helpers
{
    public static class LispaDataCityMapper
    {
        public static LocationData ToLocationData(this LispaDataCity lispaData)
        {
            return new CityData(lispaData.NomeCom)
            {
                CodIstatn = lispaData.CodIstatn,
                Fid = lispaData.Fid,
                TotaleCasi = lispaData.Totale,
                TotaleAttualmentePositivi = lispaData.Positivi,
                Deceduti = lispaData.Deceduti
            };
        }
    }
}
