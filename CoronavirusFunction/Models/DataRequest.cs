using System;
namespace CoronavirusFunction.Models
{
    public class DataRequest
    {
        public Location Location { get; set; }
        public DateTimeOffset? Date { get; set; }
    }
}
