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

        public static async Task<DatiNazionali> GetCountryData(string country, DateTime? date = null)
        {
            var pcmDcpApi = RestService.For<IPcmDpcApi>(BASEURL);
            var dati = !date.HasValue ? await pcmDcpApi.GetLastDatiNazionali() : await pcmDcpApi.GetDatiNazionali();
            return !date.HasValue ?
                dati.FirstOrDefault(x => x.Name == country) : 
                dati.FirstOrDefault(x => x.Name == country && x.Data.Date == date.Value);
        }

        public static async Task<DatiRegionali> GetAdminAreaData(string area, DateTime? date = null)
        {
            var pcmDcpApi = RestService.For<IPcmDpcApi>(BASEURL);
            var dati = !date.HasValue ? await pcmDcpApi.GetLastDatiRegionali() : await pcmDcpApi.GetDatiRegionali();
            return !date.HasValue ?
                dati.FirstOrDefault(x => x.Name == area) :
                dati.FirstOrDefault(x => x.Name == area && x.Data.Date == date.Value);
        }

        public static async Task<DatiProvinciali> GetDistrictArea(string district, DateTime? date = null)
        {
            var pcmDcpApi = RestService.For<IPcmDpcApi>(BASEURL);
            var dati = !date.HasValue ? await pcmDcpApi.GetLastDatiProvinciali() : await pcmDcpApi.GetDatiProvinciali();
            return !date.HasValue ?
                dati.FirstOrDefault(x => x.Name == district) :
                dati.FirstOrDefault(x => x.Name == district && x.Data.Date == date.Value);
        }
    }
}
