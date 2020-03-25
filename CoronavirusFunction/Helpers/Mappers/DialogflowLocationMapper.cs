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

            return new Location()
            {
                Country = locationDto.Country,
                AdminArea = locationDto.AdminArea,
                SubadminArea = subAdminArea,
                City = locationDto.City
            };
        }
    }
}
