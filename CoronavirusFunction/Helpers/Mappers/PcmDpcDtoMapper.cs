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

                TotaleCasi = countryDto.TotaleCasi,
                TotaleAttualmentePositivi = countryDto.TotaleAttualmentePositivi,
                Deceduti = countryDto.Deceduti,
                TerapiaIntensiva = countryDto.TerapiaIntensiva,
                DimessiGuariti = countryDto.DimessiGuariti,
                IsolamentoDomiciliare = countryDto.IsolamentoDomiciliare,
                NuoviAttualmentePositivi = countryDto.NuoviAttualmentePositivi,
                RicoveratiConSintomi = countryDto.RicoveratiConSintomi,
                Tamponi = countryDto.Tamponi,
                TotaleOspedalizzati = countryDto.TotaleOspedalizzati,
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
                
                TotaleCasi = adminAreaDto.TotaleCasi,
                TotaleAttualmentePositivi = adminAreaDto.TotaleAttualmentePositivi,
                Deceduti = adminAreaDto.Deceduti,
                TerapiaIntensiva = adminAreaDto.TerapiaIntensiva,
                DimessiGuariti = adminAreaDto.DimessiGuariti,
                IsolamentoDomiciliare = adminAreaDto.IsolamentoDomiciliare,
                NuoviAttualmentePositivi = adminAreaDto.NuoviAttualmentePositivi,
                RicoveratiConSintomi = adminAreaDto.RicoveratiConSintomi,
                Tamponi = adminAreaDto.Tamponi,
                TotaleOspedalizzati = adminAreaDto.TotaleOspedalizzati,
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

                TotaleCasi = subAdminAreaDto.TotaleCasi,
                TotaleAttualmentePositivi = subAdminAreaDto.TotaleAttualmentePositivi,
                Deceduti = subAdminAreaDto.Deceduti,
                TerapiaIntensiva = subAdminAreaDto.TerapiaIntensiva,
                DimessiGuariti = subAdminAreaDto.DimessiGuariti,
                IsolamentoDomiciliare = subAdminAreaDto.IsolamentoDomiciliare,
                NuoviAttualmentePositivi = subAdminAreaDto.NuoviAttualmentePositivi,
                RicoveratiConSintomi = subAdminAreaDto.RicoveratiConSintomi,
                Tamponi = subAdminAreaDto.Tamponi,
                TotaleOspedalizzati = subAdminAreaDto.TotaleOspedalizzati,
                Date = subAdminAreaDto.Date,
            };
        }
    }
}
