using CoronavirusFunction.Helpers;
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
        private const string BASEURL_PCMDCP = "https://raw.githubusercontent.com/pcm-dpc/COVID-19/master/dati-json";
        private const string BASEURL_LISPA = "https://services1.arcgis.com/XannvQVnsM1hoZyv/ArcGIS/rest/services/COMUNI_COVID19/FeatureServer/0";

        public static async Task<ItalianData> GetCoronavirusDati(Location location, DateTimeOffset? date)
        {
            date = date.Value.Date == DateTime.Now.Date ? null : (DateTimeOffset?)date.Value.Date;
            ItalianData dati = null;

            switch (location.Definition)
            {
                case LocationDefinition.Country:
                    dati = await Covid_Api.GetCountryData(location.Country, date);
                    break;
                case LocationDefinition.AdminArea:
                    dati = await Covid_Api.GetAdminAreaData(location.AdminArea, date);
                    break;
                case LocationDefinition.SubAdminArea:
                    var provincia = location.SubadminArea.Split(' ')[2];
                    dati = await Covid_Api.GetSubAdminArea(provincia, date);
                    break;
                case LocationDefinition.City:
                    dati = await Covid_Api.GetCity(location.City);
                    break;
            }

            return dati;
        }

        public static async Task<ItalianData> GetCountryData(string country, DateTimeOffset? date = null)
        {
            var pcmDpcApi = RestService.For<IPcmDpcApi>(BASEURL_PCMDCP);
            var dati = !date.HasValue ? await pcmDpcApi.GetLastItalianDataCountry() : await pcmDpcApi.GetItalianDataCountry();
            return getFilteredDati(dati, country, date);
        }

        public static async Task<ItalianData> GetAdminAreaData(string area, DateTimeOffset? date = null)
        {
            var pcmDcpApi = RestService.For<IPcmDpcApi>(BASEURL_PCMDCP);
            var dati = !date.HasValue ? await pcmDcpApi.GetLastItalianDataAdminArea() : await pcmDcpApi.GetItalianDataAdminArea();
            return getFilteredDati(dati, area, date);
        }

        public static async Task<ItalianData> GetSubAdminArea(string district, DateTimeOffset? date = null)
        {
            var pcmDcpApi = RestService.For<IPcmDpcApi>(BASEURL_PCMDCP);
            var dati = !date.HasValue ? await pcmDcpApi.GetLastItalianDataSubAdminArea() : await pcmDcpApi.GetItalianDataSubAdminArea();
            return getFilteredDati(dati, district, date);
        }

        public static async Task<ItalianData> GetCity(string city, DateTimeOffset? date = null)
        {
            if (date.HasValue)
                return null;

            var lispaApi = RestService.For<ILispaApi>(BASEURL_LISPA);
            var lispaData = await lispaApi.GetLispaData();
            var dati = new List<ItalianData>();

            foreach (var data in lispaData.Features)
                dati.Add(data.Attributes.ToItalianData());

            return getFilteredDati(dati, city, null);
        }

        private static ItalianData getFilteredDati(IEnumerable<ItalianData> dati, string name, DateTimeOffset? date)
        {
            return !date.HasValue ?
                dati.FirstOrDefault(x => x.Name.ToUpper() == name.ToUpper()) :
                dati.FirstOrDefault(x => x.Name.ToUpper() == name.ToUpper() && x.Data.Date == date.Value);
        }
    }
}
