using System;
using System.Net.Http;
namespace Akuzman.Logic
{
    public class DataRetriever
    {
        public string RawJson { get; set; }

        public string DataUrl { get; set; }

        public async System.Threading.Tasks.Task ReadByUrl ()
        {
            
            var uri = new Uri(DataUrl);

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
