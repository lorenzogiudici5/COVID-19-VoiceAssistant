using System;

namespace CoronavirusFunction.Models
{
    public partial class CountryData : LocationData
    {
        public CountryData(string name) : base(name)
        {
        }

        public override string Description => !string.IsNullOrEmpty(this.ItalianName) ? this.ItalianName : "Italia";
        public override Uri FlagUri => new Uri($"https://raw.githubusercontent.com/NovelCOVID/API/master/assets/flags/{this.Iso2.ToLower()}.png");

        public long Id { get; set; }
        public string ItalianName { get; set; }
        public string Iso2 { get; set; }
        public string Iso3 { get; set; }
    }
}
