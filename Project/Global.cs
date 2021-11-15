using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Project
{
    public static class Global
    {
        public static HttpClient httpClient = new HttpClient();

        static Global()
        {
            httpClient.BaseAddress = new Uri("https://localhost:44337/api/");
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}