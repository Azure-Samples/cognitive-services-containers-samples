using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace LanguageUnderstandingConsoleHttp
{
    class Program
    {
        private const string Endpoint = "http://localhost:5000";

        // Replace this Application ID with the ID of your Language Understanding model
        private const string ApplicationID = "da63910e-dddf-4c2f-a38f-a250a91ca176";

        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Usage : \"<Query>\"");
                Environment.Exit(-1);
            }

            var image = args[0];

            var client = new HttpClient { BaseAddress = new Uri(Endpoint) };
            DetectLanguage(client, image).Wait();
        }

        private static async Task DetectLanguage(HttpClient client, string sentence)
        {
            using (var rsp = await client.GetAsync("/luis/v2.0/apps/" + ApplicationID + "/?log=true&q=" + sentence))
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
                    Console.WriteLine(responseJson);
                }
            }
        }
    }
}
