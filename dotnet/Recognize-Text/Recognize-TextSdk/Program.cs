using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;

namespace Recognize_TextSdk
{
    class Program
    {
        // ApiKey is not needed on client side talking to a container
        private const string ApiKey = "00000000000000000000000000000000";
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

            var client = new ComputerVisionClient(new ApiKeyServiceClientCredentials(ApiKey)) { Endpoint = Endpoint };
            Task t = ExtractTextAsync(client, image);
            t.Wait();
        }

        private static async Task ExtractTextAsync(ComputerVisionClient client, string image)
        {
            // The SDK uses the 'async' POST/GET pattern. For a regular POST pattern, please see the Recognize-TextHttp sample
            using (var imageStream = File.OpenRead(image))
            {
                // Submit POST request
                var textHeaders = await client.RecognizeTextInStreamAsync(imageStream, TextRecognitionMode.Printed);

                // Submit GET requests to poll for result
                await GetTextAsync(client, textHeaders.OperationLocation);
            }
        }

        private static async Task GetTextAsync(ComputerVisionClient computerVision, string operationLocation)
        {
            var operationId = operationLocation.Substring(operationLocation.LastIndexOf('/') + 1);
            while (true)
            {
                var result = await computerVision.GetTextOperationResultAsync(operationId);
                Console.WriteLine("Polling for result...");
                if (result.Status == TextOperationStatusCodes.Succeeded)
                {
                    foreach (var line in result.RecognitionResult.Lines)
                    {
                        Console.WriteLine(line.Text);
                    }
                    break;
                }

                await Task.Delay(1000);
            }
        }
    }
}
