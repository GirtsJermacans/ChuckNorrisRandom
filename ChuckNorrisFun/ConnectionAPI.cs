using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ChuckNorrisFun
{
    /*
     * Youtube channel "IAmTimCorey" had plenty of good info on how to use System.Net / System.Net.Http / System.Threading.Tasks to set up HttpClient
     * to use HttpClient installed NuGet Package - WebApi.Client
     * opening TCP/IP port - HttpClient static - handles concurrency
     * MediaTypeWithQualityHeaderValue - sets up headers that require json format
    */

    public static class ConnectionAPI
    {
        // attributes
        public static HttpClient Client { get; set; }

        // operations
        public static void InitializeClient()
        {
            if (Client == null)
            {
                Client = new HttpClient();
                Client.DefaultRequestHeaders.Accept.Clear();
                Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }
        }

    }
}
