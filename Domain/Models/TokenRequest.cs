using Newtonsoft.Json;

namespace Domain.Models
{
    [JsonObject("tokenRequest")]
    public class TokenRequest
    {
        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
