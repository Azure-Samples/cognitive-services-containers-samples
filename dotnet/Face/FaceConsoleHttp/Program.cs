using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace FaceConsoleHttp
{
    class Program
    {
        private const string Endpoint = "http://localhost:5000";

        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage : <Image>");
                Console.WriteLine("  Image: Local file path for image containing one or more faces.");
                Environment.Exit(-1);
            }

            var image = args[1];
            if (!File.Exists(image))
            {
                Console.WriteLine($"Unable to open or read '{image}'");
                Environment.Exit(-1);
            }

            var client = new HttpClient { BaseAddress = new Uri(Endpoint) };
            DetectFaces(client, image).Wait();
        }

        private static async Task DetectFaces(HttpClient client, string image)
        {
            using (var imageStream = File.OpenRead(image))
            {
                var reqBody = new StreamContent(imageStream);
                reqBody.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
                using (var rsp = await client.PostAsync("/face/v1.0/detect?returnFaceAttributes=*", reqBody))
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
