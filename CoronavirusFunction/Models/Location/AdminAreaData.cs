using System;

namespace CoronavirusFunction.Models
{
    public partial class AdminAreaData : LocationData
    {
        public AdminAreaData(string name) : base(name)
        {
        }

        public override string Description => this.Name;
        public override Uri FlagUri => new Uri($"https://www.novalibandiere.it/wp-content/uploads/{this.Name.ToLower()}.gif");

        public long CodiceRegione { get; set; }
    }
}
