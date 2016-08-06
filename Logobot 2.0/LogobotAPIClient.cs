using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Logobot2_0
{
    class LogobotAPIClient
    {
        private static string apiURI = "http://logobotapi2020160416054217.azurewebsites.net/";
        //private static string apiURI = "http://localhost:61475/";

        public HttpClient getHTTPClient()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(apiURI); 
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }
    }
}
