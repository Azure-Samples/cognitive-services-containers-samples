using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Recognize_TextHttp
{
    class Program
    {
        private const string Endpoint = "http://localhost:5000";

        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage : <Image>");
                Console.WriteLine("  Image: Local file path for image containing printed text.");
                Environment.Exit(-1);
            }

            var image = args[1];
            if (!File.Exists(image))
            {
                Console.WriteLine($"Unable to open or read '{image}'");
                Environment.Exit(-1);
            }

            var client = new HttpClient { BaseAddress = new Uri(Endpoint) };
            ExtractText(client, image).Wait();
        }

        private static async Task ExtractText(HttpClient client, string image)
        {
            using (var imageStream = File.OpenRead(image))
            {
                var reqBody = new StreamContent(imageStream);
                reqBody.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
                using (var rsp = await client.PostAsync("/vision/v2.0/recognizetextDirect", reqBody))
                {
                    if (!rsp.IsSuccessStatusCode)
                    {
                        var error = await rsp.Content.ReadAsStringAsync();
                        Console.WriteLine($"Request failed: {error}");
                    }
                    else
                    {
                        var rspBody = await rsp.Content.ReadAsStringAsync();
                        var rspJson = JToken.Parse(rspBody);
                        Console.WriteLine(rspJson);
                    }
                }
            }
        }
    }
}
