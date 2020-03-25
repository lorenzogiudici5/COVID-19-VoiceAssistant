using CoronavirusFunction.Models;

namespace CoronavirusFunction.Helpers
{
    public static class LispaDataCityMapper
    {
        public static ItalianData ToItalianData(this LispaDataCity lispaData)
        {
            return new ItalianDataCity()
            {
                Comune = lispaData.NomeCom,
                CodIstatn = lispaData.CodIstatn,
                Fid = lispaData.Fid,
                TotaleCasi = lispaData.Totale,
                TotaleAttualmentePositivi = lispaData.Positivi,
                Deceduti = lispaData.Deceduti
            };
        }
    }
}
