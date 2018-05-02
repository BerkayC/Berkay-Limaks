using System;
using System.Net.Http;
using Newtonsoft.Json;
namespace Akuzman.Logic
{
    public class DataRetriever
    {
        public string RawJson { get; set; }

        public string DataUrl { get; set; }

        public async System.Threading.Tasks.Task ReadByUrl (string url)
        {
            var uri = new Uri(url);

            HttpClient myClient = new HttpClient();

            var response = await myClient.GetAsync(uri);

            var content = await response.Content.ReadAsStringAsync();

            RawJson = (String)content;

        }

        public  DataRetriever(string url)
        {
            DataUrl = url;
        }


    }
}
