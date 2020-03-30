using CoronavirusFunction.Models;

namespace CoronavirusFunction.Helpers
{
    public static class PcmDpcDtoMapper
    {
        public static CountryData ToCountryData(this PcmDpcCountryDto countryDto)
        {
            if (countryDto == null)
                return null;

            return new CountryData(countryDto.Description)
            {
                ItalianName = countryDto.Description,
                Country = countryDto.Stato,
                Iso2 = "it",
                Iso3 = "ita",
                //Lat = countryDto.Lat,
                //Long = countryDto.Long,
                //FlagUri = "",

                Cases = countryDto.TotaleCasi,
                Active = countryDto.TotaleAttualmentePositivi,
                Deaths = countryDto.Deceduti,
                Critical = countryDto.TerapiaIntensiva,
                Recovered = countryDto.DimessiGuariti,
                Quarantine = countryDto.IsolamentoDomiciliare,
                TodayCases = countryDto.NuoviAttualmentePositivi,
                NotCritical = countryDto.RicoveratiConSintomi,
                Test = countryDto.Tamponi,
                Hospitalised = countryDto.TotaleOspedalizzati,
                Date = countryDto.Date,               
            };
        }

        public static AdminAreaData ToAdminAreaData(this PcmDpcAdminAreaDto adminAreaDto)
        {
            if (adminAreaDto == null)
                return null;

            return new AdminAreaData(adminAreaDto.Description)
            {
                Country = adminAreaDto.Stato,
                Lat = adminAreaDto.Lat,
                Long = adminAreaDto.Long,
                CodiceRegione = adminAreaDto.CodiceRegione,
                
                Cases = adminAreaDto.TotaleCasi,
                Active = adminAreaDto.TotaleAttualmentePositivi,
                Deaths = adminAreaDto.Deceduti,
                Critical = adminAreaDto.TerapiaIntensiva,
                Recovered = adminAreaDto.DimessiGuariti,
                Quarantine = adminAreaDto.IsolamentoDomiciliare,
                TodayCases = adminAreaDto.NuoviAttualmentePositivi,
                NotCritical = adminAreaDto.RicoveratiConSintomi,
                Test = adminAreaDto.Tamponi,
                Hospitalised = adminAreaDto.TotaleOspedalizzati,
                Date = adminAreaDto.Date,
            };
        }

        public static SubAdminAreaData ToSubAdminAreaData(this PcmDpcSubAdminAreaDto subAdminAreaDto)
        {
            if (subAdminAreaDto == null)
                return null;

            return new SubAdminAreaData(subAdminAreaDto.Description)
            {
                Country = subAdminAreaDto.Stato,
                Lat = subAdminAreaDto.Lat,
                Long = subAdminAreaDto.Long,
                SiglaProvincia = subAdminAreaDto.SiglaProvincia,
                CodiceProvincia = subAdminAreaDto.CodiceProvincia,
                Region = subAdminAreaDto.DenominazioneRegione,
                CodiceRegione = subAdminAreaDto.CodiceRegione,

                Cases = subAdminAreaDto.TotaleCasi,
                Active = subAdminAreaDto.TotaleAttualmentePositivi,
                Deaths = subAdminAreaDto.Deceduti,
                Critical = subAdminAreaDto.TerapiaIntensiva,
                Recovered = subAdminAreaDto.DimessiGuariti,
                Quarantine = subAdminAreaDto.IsolamentoDomiciliare,
                TodayCases = subAdminAreaDto.NuoviAttualmentePositivi,
                NotCritical = subAdminAreaDto.RicoveratiConSintomi,
                Test = subAdminAreaDto.Tamponi,
                Hospitalised = subAdminAreaDto.TotaleOspedalizzati,
                Date = subAdminAreaDto.Date,
            };
        }
    }
}
