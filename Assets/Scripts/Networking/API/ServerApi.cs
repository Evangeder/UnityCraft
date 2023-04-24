using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;

namespace UnityCraft.Networking.API
{
    public class ServerApi
    {
        private const string API_SITE = "https://www.classicube.net/api/servers/";

        public async Task<ServerApiResult> Fetch()
        {
            string json;

            using (WebClient client = new WebClient())
            {
                json = await client.DownloadStringTaskAsync(API_SITE);
            }

            return JsonConvert.DeserializeObject<ServerApiResult>(json);
        }
    }
}
