using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Recognize_Text_Direct
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage : <Program> <Endpoint> <Image path>");
                Environment.Exit(-1);
            }

            Task t = ExtractLocalTextSync(args[0], args[1]);
            t.Wait();
        }

        private static async Task ExtractLocalTextSync(string endpoint, string imagePath)
        {
            using (var imageStream = File.OpenRead(imagePath))
            using (var imageContent = new StreamContent(imageStream))
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(endpoint);
                var requestAddress = "/vision/v2.0/recognizetextDirect";
                imageContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                using (var response = await client.PostAsync(requestAddress, imageContent))
                {
                    var resultAsString = await response.Content.ReadAsStringAsync();
                    var resultAsJson = JsonConvert.DeserializeObject<JObject>(resultAsString);
                    foreach (var line in resultAsJson["lines"])
                    {
                        Console.WriteLine(line["text"]);
                        Console.WriteLine();
                    }
                }
            }
        }
    }
}
