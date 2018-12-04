using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace LanguageConsoleHttp
{
    class Program
    {
        private const string Endpoint = "http://localhost:5000";

        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Usage : <Phrase>");
                Environment.Exit(-1);
            }

            var phrase = args[0];

            var client = new HttpClient { BaseAddress = new Uri(Endpoint) };
            DetectLanguage(client, phrase).Wait();
        }

        private static async Task DetectLanguage(HttpClient client, string phrase)
        {
            var content = new StringContent("{\"documents\":[{\"id\":\"1\", \"text\":\"" + phrase + "\"}]}");
            content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
            
            using (var rsp = await client.PostAsync("/text/analytics/v2.0/languages", content))
            {
                if (!rsp.IsSuccessStatusCode)
                {
                    var error = await rsp.Content.ReadAsStringAsync();
                    Console.WriteLine($"Request failed: {error}");
                }
                else
                {
                    var responseBody = await rsp.Content.ReadAsStringAsync();
                    var responseJson = JToken.Parse(responseBody);
                    Console.WriteLine($"Detected language: {responseJson["documents"][0]["detectedLanguages"][0]["name"]}");
                }
            }
        }
    }
}
