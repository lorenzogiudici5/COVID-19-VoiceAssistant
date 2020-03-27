using CoronavirusFunction.Models;

namespace CoronavirusFunction.Helpers
{
    public static class DialogflowLocationMapper
    {
        public static Location ToLocation(this DialogflowLocationDto locationDto)
        {
            var subAdminArea = !string.IsNullOrEmpty(locationDto.SubadminArea) ? locationDto.SubadminArea : 
                (!string.IsNullOrEmpty(locationDto.BusinessName) && !string.IsNullOrEmpty(locationDto.StreetAddress)) ? 
                $"{locationDto.BusinessName} di {locationDto.StreetAddress}" :
                (!string.IsNullOrEmpty(locationDto.BusinessName) && !string.IsNullOrEmpty(locationDto.City)) ?
                $"{locationDto.BusinessName} di {locationDto.City}" :
                default(string);

            var definition =
                !string.IsNullOrEmpty(locationDto.AdminArea) ? LocationDefinition.AdminArea :
                !string.IsNullOrEmpty(subAdminArea) ? LocationDefinition.SubAdminArea :
                !string.IsNullOrEmpty(locationDto.Country) ? LocationDefinition.Country :
                LocationDefinition.City;

            var name = definition switch
            {
                LocationDefinition.Country => locationDto.Country,
                LocationDefinition.AdminArea => locationDto.AdminArea,
                LocationDefinition.SubAdminArea => locationDto.SubadminArea,
                LocationDefinition.City => locationDto.City,
                _ => string.Empty,
            };

            return new Location(name, definition);
        }
    }
}
