using CoronavirusFunction.Helpers;
using CoronavirusFunction.Helpers.Mappers;
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
        private const string BASEURL_NOVEL = "https://corona.lmao.ninja";

        public static async Task<LocationData> GetCoronavirusDati(Location location, DateTimeOffset? date)
        {
            date = date.Value.Date == DateTime.Now.Date ? null : (DateTimeOffset?)date.Value.Date;
            LocationData dati = null;

            switch (location.Definition)
            {
                case LocationDefinition.World:
                    dati = await Covid_Api.GetWorldData();
                    break;
                case LocationDefinition.Country:
                    dati = await Covid_Api.GetCountryData(location.Name, date); 
                    break;
                case LocationDefinition.AdminArea:
                    dati = await Covid_Api.GetAdminAreaData(location.Name, date);
                    break;
                case LocationDefinition.SubAdminArea:
                    var provincia = location.Name.Split(' ')[2];
                    dati = await Covid_Api.GetSubAdminArea(provincia, date);
                    break;
                case LocationDefinition.City:
                    dati = await Covid_Api.GetCity(location.Name);
                    break;
            }

            return dati;
        }

        public static async Task<LocationData> GetWorldData(DateTimeOffset? date = null)
        {
            var novelApi = RestService.For<INovelCovidApi>(BASEURL_NOVEL);
            var novelData = await novelApi.GeWorldData();
            return novelData.ToWorldData();
        }

        public static async Task<LocationData> GetCountryData(string country, DateTimeOffset? date = null)
        {
            if(country.ToUpper() == "ITALIA")
            {
                var pcmDpcApi = RestService.For<IPcmDpcApi>(BASEURL_PCMDCP);
                var dati = !date.HasValue ? await pcmDpcApi.GetLastItalianDataCountry() : await pcmDpcApi.GetItalianDataCountry();
                
                var pcmDpcCountry = getFilteredDati(dati, country, date) as PcmDpcCountryDto;
                return pcmDpcCountry.ToCountryData();
            }
            else
            {
                var code = CountryHelper.Countries.Where(x => x.ItalianName == country).FirstOrDefault()?.Alpha2;

                var novelApi = RestService.For<INovelCovidApi>(BASEURL_NOVEL);
                var novelData = await novelApi.GeCountryData(code);
                return novelData.ToCountryData(country);
            }
        }

        public static async Task<LocationData> GetAdminAreaData(string area, DateTimeOffset? date = null)
        {
            var pcmDcpApi = RestService.For<IPcmDpcApi>(BASEURL_PCMDCP);
            var dati = !date.HasValue ? await pcmDcpApi.GetLastItalianDataAdminArea() : await pcmDcpApi.GetItalianDataAdminArea();
            var pcmDpcAdminArea = getFilteredDati(dati, area, date) as PcmDpcAdminAreaDto;

            return pcmDpcAdminArea.ToAdminAreaData();
        }

        public static async Task<LocationData> GetSubAdminArea(string district, DateTimeOffset? date = null)
        {
            var pcmDcpApi = RestService.For<IPcmDpcApi>(BASEURL_PCMDCP);
            var dati = !date.HasValue ? await pcmDcpApi.GetLastItalianDataSubAdminArea() : await pcmDcpApi.GetItalianDataSubAdminArea();
            var pcmDpcSubAdminAdminArea = getFilteredDati(dati, district, date) as PcmDpcSubAdminAreaDto;

            return pcmDpcSubAdminAdminArea.ToSubAdminAreaData();
        }

        public static async Task<LocationData> GetCity(string city, DateTimeOffset? date = null)
        {
            if (date.HasValue)
                return null;

            var lispaApi = RestService.For<ILispaApi>(BASEURL_LISPA);
            var lispaData = await lispaApi.GetLispaData();
            var dati = new List<LocationData>();

            foreach (var data in lispaData.Features)
                dati.Add(data.Attributes.ToLocationData());

            return dati.FirstOrDefault(x => x.Description.ToUpper() == city.ToUpper());
        }

        private static PcmDpcDto getFilteredDati(IEnumerable<PcmDpcDto> dati, string name, DateTimeOffset? date)
        {
            return !date.HasValue ?
                dati.FirstOrDefault(x => x.Description.ToUpper() == name.ToUpper()) :
                dati.FirstOrDefault(x => x.Description.ToUpper() == name.ToUpper() && x.Date.Date == date.Value);
        }
    }
}
