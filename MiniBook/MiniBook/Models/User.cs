using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniBook.Models
{
    public class User
    {
        private string _picture;

        [JsonProperty("sub")]
        public Guid Sub { get; set; }

        [JsonProperty("given_name")]
        public string FirstName { get; set; }

        [JsonProperty("family_name")]
        public string LastName { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("birthdate")]
        public string DoB { get; set; }

        [JsonProperty("preferred_username")]
        public string PreferredUsername { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("picture")]
        public string Picture
        {
            get => _picture ?? "default_profile_picture_grey_male_icon.png";
            set => _picture = value;
        }

        [JsonProperty("email_verified")]
        public bool EmailVerified { get; set; }
    }
}
