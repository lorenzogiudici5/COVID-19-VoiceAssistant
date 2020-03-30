using System;
using System.Linq;

namespace CoronavirusFunction.Models
{
    public abstract class LocationData
    {
        public LocationData (string name)
        {
            this.Name = name;
        }

        // SET CULTURE
        public string Name { get; private set; }
        public virtual string Description => this.Name;
        public virtual Uri FlagUri { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public string Country { get; set; }
        public DateTimeOffset? Date { get; set; }

        public long? RicoveratiConSintomi { get; set; }
        public long? TerapiaIntensiva { get; set; }
        public long? TotaleOspedalizzati { get; set; }
        public long? IsolamentoDomiciliare { get; set; }
        public long? TotaleAttualmentePositivi { get; set; }
        public long? NuoviAttualmentePositivi { get; set; }
        public long? DimessiGuariti { get; set; }
        public long? Deceduti { get; set; }
        public long? TotaleCasi { get; set; }
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
        public string ToShortStringNewPositive() => this.NuoviAttualmentePositivi != null ? $"Nuovi positivi: {NuoviAttualmentePositivi}" : default(string);


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
                this.ToShortStringNewPositive(),
                this.ToShortStringTest(),
                this.ToShortStringRecovered(),
                this.ToShortStringHospitalized(),
                this.ToShortStringTherapy()
            };
            return string.Join($".  {Environment.NewLine}", availableData.Where(x => !string.IsNullOrEmpty(x)));
        }
    }
}
