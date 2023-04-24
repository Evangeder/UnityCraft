using Newtonsoft.Json;
using System.Linq;

namespace UnityCraft.Networking.API
{
    public class ServerApiResult
    {
        [JsonProperty(PropertyName = "servers")]
        public Servers[] servers { get; set; }

        public void SortByPlayersOnline()
        {
            servers = servers.OrderByDescending(server => server.CurrentPlayers).ToArray();
        }
    }
}