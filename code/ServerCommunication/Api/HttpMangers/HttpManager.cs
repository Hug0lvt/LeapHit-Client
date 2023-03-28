using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerCommunication.Api.HttpMangers
{
    public class HttpManager
    {
        protected readonly HttpClient _client;

        public HttpManager(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri("https://codefirst.iut.uca.fr");
        }
    }
}
