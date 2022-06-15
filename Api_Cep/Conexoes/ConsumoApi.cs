using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace ApiCep.Conexoes
{
    public static class ConsumoApi
    {
        private static readonly HttpClient _client = new HttpClient();

        public static T Get<T>(string url)
        {
            var response = _client.GetAsync(url).GetAwaiter().GetResult();

            var content = response.Content.ReadAsStringAsync().Result;

            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<T>(content);
            } 
           
            throw new Exception("Serviço indisponível no momento.");
            
        }

        public static T Post<T>(string url, object obj)
        {
            string json = JsonConvert.SerializeObject(obj);
            var objContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = _client.PostAsync(url, objContent).GetAwaiter().GetResult();

            var content = response.Content.ReadAsStringAsync().Result;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<T>(content);
            }

            throw new Exception("Serviço indisponível no momento.");

        }
    }
}
