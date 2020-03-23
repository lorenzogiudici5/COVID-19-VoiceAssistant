using CoronavirusFunction.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoronavirusFunction.Services
{
    public static class Covid_Api
    {
        private const string BASEURL = "https://raw.githubusercontent.com/pcm-dpc/COVID-19/master/dati-json";

        public static async Task<Dati> GetCoronavirusDati(Location location, DateTime? date = null)
        {
            Dati dati = null;
            // TODO: date filter
            switch (location.Definition)
            {
                case LocationDefinition.Paese:
                    dati = await Covid_Api.GetCountryData(location.Country, date);
                    break;
                case LocationDefinition.Regione:
                    dati = await Covid_Api.GetAdminAreaData(location.AdminArea, date);
                    break;
                case LocationDefinition.Provincia:
                    var provincia = location.SubadminArea.Split(' ')[2];
                    dati = await Covid_Api.GetDistrictArea(provincia, date);
                    break;
            }

            return dati;
        }

        public static async Task<Dati> GetCountryData(string country, DateTime? date = null)
        {
            var pcmDpcApi = RestService.For<IPcmDpcApi>(BASEURL);
            var dati = !date.HasValue ? await pcmDpcApi.GetLastDatiNazionali() : await pcmDpcApi.GetDatiNazionali();
            return getFilteredDati(dati, country, date);
        }

        public static async Task<Dati> GetAdminAreaData(string area, DateTime? date = null)
        {
            var pcmDcpApi = RestService.For<IPcmDpcApi>(BASEURL);
            var dati = !date.HasValue ? await pcmDcpApi.GetLastDatiRegionali() : await pcmDcpApi.GetDatiRegionali();
            return getFilteredDati(dati, area, date);
        }

        public static async Task<Dati> GetDistrictArea(string district, DateTime? date = null)
        {
            var pcmDcpApi = RestService.For<IPcmDpcApi>(BASEURL);
            var dati = !date.HasValue ? await pcmDcpApi.GetLastDatiProvinciali() : await pcmDcpApi.GetDatiProvinciali();
            return getFilteredDati(dati, district, date);
        }

        private static Dati getFilteredDati(IEnumerable<Dati> dati, string name, DateTime? date)
        {
            return !date.HasValue ?
                dati.FirstOrDefault(x => x.Name.ToUpper() == name.ToUpper()) :
                dati.FirstOrDefault(x => x.Name.ToUpper() == name.ToUpper() && x.Data.Date == date.Value);
        }
    }
}
