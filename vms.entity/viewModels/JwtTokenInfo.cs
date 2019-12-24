using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace vms.entity.viewModels
{
    public class JwtTokenInfo
    {
        public string grant_type { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string refreshtoken { get; set; }
        public string name { get; set; }
        public string clientName { get; set; }
        public string email { get; set; }
        public string clientId { get; set; }
        
    }
    [JsonObject("tokenManagement")]
    public class TokenManagement
    {
        [JsonProperty("secret")]
        public string Secret { get; set; }

        [JsonProperty("issuer")]
        public string Issuer { get; set; }

        [JsonProperty("audience")]
        public string Audience { get; set; }

        [JsonProperty("accessExpiration")]
        public int AccessExpiration { get; set; }

        [JsonProperty("refreshExpiration")]
        public int RefreshExpiration { get; set; }

    }
}
