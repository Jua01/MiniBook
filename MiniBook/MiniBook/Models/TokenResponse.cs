﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniBook.Models
{
    public class TokenResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }
        [JsonProperty("error")]
        public string Error { get; set; }
        [JsonProperty("error_description")]
        public string ErrorDescription { get; set; }
        public DateTime ExpiresAt { get; internal set; }

        internal bool IsExpired()
        {
            return DateTime.UtcNow < ExpiresAt;
        }
    }
}
