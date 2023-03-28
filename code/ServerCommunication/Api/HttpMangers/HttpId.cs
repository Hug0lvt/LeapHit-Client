using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ServerCommunication.Api.HttpMangers
{
    public class HttpId : HttpManager
    {
        private const string UrlApiId = "/containers/leap-hit-team-server-container/api/PlayersConnexion";
        private const string Token="K02q7naLzjmodzAFfoSO4mPydr7W5hydPMrHtA6D";


        public HttpId(HttpClient client) : base(client) { }

        public async Task<string?> GetId()
        {
            var response = await _client.GetAsync($"{UrlApiId}/{Token}");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var resp = response.Content.ReadAsStringAsync();
                return  await resp;
            }
            return null;
        }
    }
}
