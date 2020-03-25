using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoronavirusFunction.Models
{
    public class DialogflowUserDto
    {
        [JsonProperty("userId")]
        public string UserId { get; set; }
        [JsonProperty("locale")]
        public string Locale { get; set; }

        [JsonProperty("lastSeen")]
        public DateTimeOffset? LastSeen { get; set; }

        [JsonProperty("userVerificationStatus")]
        public string UserVerificationStatus { get; set; }
    }
}
