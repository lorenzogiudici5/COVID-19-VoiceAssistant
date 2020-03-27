﻿using Newtonsoft.Json;
using System;

namespace CoronavirusFunction.Models
{
    public class User
    {
        public string UserId { get; set; }

        public DateTimeOffset? LastSeen { get; set; }
        public bool IsReturningUser => LastSeen.HasValue;
        public string Language { get; set; }
    }
}
