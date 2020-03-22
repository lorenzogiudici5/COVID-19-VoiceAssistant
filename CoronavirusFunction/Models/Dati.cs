using Newtonsoft.Json;
using System;

namespace CoronavirusFunction.Models
{
    public abstract class Dati
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

        // TODO: to string Contagiati/Deceduti/Attualmente
    }
}
