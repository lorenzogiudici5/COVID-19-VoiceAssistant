using Newtonsoft.Json;
using System;
using System.Linq;

namespace CoronavirusFunction.Models
{
    public abstract class ItalianData
    {
        public abstract string Name { get; }

        [JsonProperty("stato")]
        public string Stato { get; set; }

        [JsonProperty("data")]
        public DateTimeOffset Data { get; set; }

        [JsonProperty("ricoverati_con_sintomi")]
        public long? RicoveratiConSintomi { get; set; }

        [JsonProperty("terapia_intensiva")]
        public long? TerapiaIntensiva { get; set; }

        [JsonProperty("totale_ospedalizzati")]
        public long? TotaleOspedalizzati { get; set; }

        [JsonProperty("isolamento_domiciliare")]
        public long? IsolamentoDomiciliare { get; set; }

        [JsonProperty("totale_attualmente_positivi")]
        public long? TotaleAttualmentePositivi { get; set; }

        [JsonProperty("nuovi_attualmente_positivi")]
        public long? NuoviAttualmentePositivi { get; set; }

        [JsonProperty("dimessi_guariti")]
        public long? DimessiGuariti { get; set; }

        [JsonProperty("deceduti")]
        public long? Deceduti { get; set; }

        [JsonProperty("totale_casi")]
        public long? TotaleCasi { get; set; }

        [JsonProperty("tamponi")]
        public long? Tamponi { get; set; }

        public string ToLongStringConfirmed() => this.TotaleCasi != null ? $"Il totale dei casi confermati è {TotaleCasi}" : default(string);
        public string ToLongStringPositive() => this.TotaleAttualmentePositivi != null ? $"Il numero dei positivi è {TotaleAttualmentePositivi}" : default(string);
        public string ToLongStringDeaths() => this.Deceduti != null ? $"Il numero dei deceduti è {Deceduti}" : default(string);

        public string ToShortStringConfirmed() => this.TotaleCasi != null ? $"Totale casi: {TotaleCasi}" : default(string);
        public string ToShortStringPositive() => this.TotaleAttualmentePositivi != null ? $"Attualmente Positivi: {TotaleAttualmentePositivi}" : default(string);
        public string ToShortStringDeaths() => this.Deceduti != null ? $"Deceduti: {Deceduti}" : default(string);
        public string ToShortStringTest() => this.Tamponi != null ? $"Tamponi: {Tamponi}" : default(string);
        public string ToShortStringRecovered() => this.DimessiGuariti != null ? $"Guariti: {DimessiGuariti}" : default(string);
        public string ToShortStringHospitalized() => this.TotaleOspedalizzati != null ? $"Ospedalizzati: {TotaleOspedalizzati}" : default(string);
        public string ToShortStringTherapy() => this.TerapiaIntensiva != null ? $"Terapia intensiva: {TerapiaIntensiva}" : default(string);
        

        public string ToSpeechSummary(LocationDefinition locationDefinition)
        {
            return locationDefinition == LocationDefinition.SubAdminArea ?
                this.ToLongStringConfirmed() :
                string.Join($".  {Environment.NewLine}", 
                    this.ToLongStringConfirmed(), 
                    this.ToLongStringPositive(), 
                    this.ToLongStringDeaths());
        }
        public string ToTextSummary(LocationDefinition locationDefinition)
        {
            var availableData = new string[]
            {
                this.ToShortStringConfirmed(),
                this.ToShortStringPositive(),
                this.ToShortStringDeaths(),
                this.ToShortStringTest(),
                this.ToShortStringRecovered(),
                this.ToShortStringHospitalized(),
                this.ToShortStringTherapy()
            };
            return string.Join($".  {Environment.NewLine}", availableData.Where(x => !string.IsNullOrEmpty(x)));
        }
    }
}
