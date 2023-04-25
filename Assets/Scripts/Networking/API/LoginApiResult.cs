using Newtonsoft.Json;

namespace UnityCraft.Networking.API
{
    class LoginApiResult
    {
        [JsonProperty(PropertyName = "authenticated")]
        public bool Authenticated { get; set; }
        [JsonProperty(PropertyName = "errors")]
        public string[] Errors { get; set; }
        [JsonProperty(PropertyName = "token")]
        public string Token { get; set; }
        [JsonProperty(PropertyName = "username")]
        public string Ussername { get; set; }
    }
}