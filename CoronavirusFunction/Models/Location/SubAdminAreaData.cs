namespace CoronavirusFunction.Models
{
    public class SubAdminAreaData : LocationData
    {
        public SubAdminAreaData(string name) : base(name)
        {
        }

        public override string Description => $"Provincia di {this.Name}";
        public long CodiceRegione { get; set; }
        public string Region { get; set; }
        public long CodiceProvincia { get; set; }
        public string SiglaProvincia { get; set; }
    }
}
