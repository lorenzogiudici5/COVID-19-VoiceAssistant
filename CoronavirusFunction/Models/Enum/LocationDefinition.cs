using System.ComponentModel;

namespace CoronavirusFunction.Models
{
    public enum LocationDefinition
    {
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
