using System.ComponentModel;

namespace CoronavirusFunction.Models
{
    public enum LocationDefinition
    {
        [Description("Mondo")]
        World,
        [Description("Paese")]
        Country,
        [Description("Regione")]
        AdminArea,
        [Description("Provincia")]
        SubAdminArea,
        [Description("Città")]
        City
    }
}
