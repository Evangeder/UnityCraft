using Newtonsoft.Json;

namespace UnityCraft.Networking.API
{
    public class Servers
    {
        [JsonProperty(PropertyName = "country_abbr")]
        public string FlagCode { get; set; }
        [JsonProperty(PropertyName = "featured")]
        public bool Featured { get; set; }
        [JsonProperty(PropertyName = "hash")]
        public string Hash { get; set; }
        [JsonProperty(PropertyName = "maxplayers")]
        public int MaxPlayers { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "players")]
        public int CurrentPlayers { get; set; }
        [JsonProperty(PropertyName = "software")]
        public string Software { get; set; }
        [JsonProperty(PropertyName = "web")]
        public bool Web { get; set; }
        [JsonProperty(PropertyName = "ip")]
        public string IP { get; set; }
        [JsonProperty(PropertyName = "port")]
        public int Port { get; set; }
    }
}