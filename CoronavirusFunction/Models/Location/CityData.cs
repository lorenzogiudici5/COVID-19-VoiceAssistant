namespace CoronavirusFunction.Models
{
    public class CityData : LocationData
    {
        public CityData(string name) : base(name)
        {
        }

        public override string Description => this.Name;

        public long Fid { get; set; }
        public string CodIstatn { get; set; }
        public ProvinceLombardia Provincia { get; set; }
    }
}
