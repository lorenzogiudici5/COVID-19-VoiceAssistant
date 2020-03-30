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
        public DateTimeOffset Date { get; set; }

        public long? NotCritical { get; set; }
        public long? Critical { get; set; }
        public long? Hospitalised { get; set; }
        public long? Quarantine { get; set; }
        public long? Active { get; set; }
        public long? TodayCases { get; set; }
        public long? Recovered { get; set; }
        public long? Deaths { get; set; }
        public long? Cases { get; set; }
        public long? Test { get; set; }
        public float? MortalityRate => Deaths != 0 && Cases != 0 ?  ((float) Deaths / Cases)*100 : default(float?);

        public string ToLongStringConfirmed() => this.Cases != null ? $"Il totale dei casi confermati è {Cases}" : default(string);
        public string ToLongStringPositive() => this.Active != null ? $"Il numero dei positivi è {Active}" : default(string);
        public string ToLongStringDeaths() => this.Deaths != null ? $"Il numero dei deceduti è {Deaths}" : default(string);

        public string ToShortStringConfirmed() => this.Cases != null ? $"Totale casi: {Cases}" : default(string);
        public string ToShortStringPositive() => this.Active != null ? $"Attualmente Positivi: {Active}" : default(string);
        public string ToShortStringDeaths() => this.Deaths != null ? $"Deceduti: {Deaths}" : default(string);
        public string ToShortStringTest() => this.Test != null ? $"Tamponi: {Test}" : default(string);
        public string ToShortStringRecovered() => this.Recovered != null ? $"Guariti: {Recovered}" : default(string);
        public string ToShortStringHospitalized() => this.Hospitalised != null ? $"Ospedalizzati: {Hospitalised}" : default(string);
        public string ToShortStringTherapy() => this.Critical != null ? $"Terapia intensiva: {Critical}" : default(string);
        public string ToShortStringNewPositive() => this.TodayCases != null ? $"Nuovi positivi: {TodayCases}" : default(string);
        public string ToShortStringMortalityRate() => this.MortalityRate != null ? $"Tasso di mortalità: {MortalityRate}% " : default(string);


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
                this.ToShortStringMortalityRate(),
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
