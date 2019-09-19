using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gallery.ViewModels
{
    [JsonObject("registerUserViewModel")]
    public class RegisterUserViewModel
    {
        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("passwordHash")]
        public string PasswordHash { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
    }
}
