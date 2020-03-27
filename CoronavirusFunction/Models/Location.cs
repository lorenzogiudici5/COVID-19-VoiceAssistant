using System.Globalization;

namespace CoronavirusFunction.Models
{
    public class Location
    {
        public Location (string name, LocationDefinition definition)
        {
            this.Name = name;
            this.Definition = definition;
        }
        public string Name { get; set; }
        //public string AdminArea { get; set; }
        //public string City { get; set; }
        //public string SubadminArea { get; set; }
        //public string Country { get; set; }

        public LocationDefinition Definition { get; private set; }
        //public LocationDefinition Definition => getLocationDefinition();

        //private LocationDefinition getLocationDefinition()
        //{
        //    return
        //        this.AdminArea != null ? LocationDefinition.AdminArea :
        //        this.SubadminArea != null ? LocationDefinition.SubAdminArea :
        //        this.Country != null ? LocationDefinition.Country :
        //        LocationDefinition.City;
        //}
    }
}
