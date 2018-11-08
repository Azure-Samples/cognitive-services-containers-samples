using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using System.Net.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Recognize_Text_Async
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("Usage : <Program> <Endpoint> <SubscriptionKey> <Image path>");
                Environment.Exit(-1);
            }

            Task t = ExtractLocalTextAsync(args[0], args[1], args[2]);
            t.Wait();
        }

        private static async Task ExtractLocalTextAsync(string endpoint, string subscriptionKey, string imagePath)
        {
            if (!File.Exists(imagePath))
            {
                Console.WriteLine("Unable to open or read image:{0}", imagePath);
                Environment.Exit(-1);
            }

            // The Recognize Text container supports only TextRecognitionMode.Printed
            const TextRecognitionMode textRecognitionMode = TextRecognitionMode.Printed;

            ComputerVisionClient computerVision = new ComputerVisionClient(
                new ApiKeyServiceClientCredentials(subscriptionKey),
                new DelegatingHandler[] { })
            {
                Endpoint = endpoint
            };

            using (Stream imageStream = File.OpenRead(imagePath))
            {
                // Start the async process to recognize the text
                var textHeaders = await computerVision.RecognizeTextInStreamAsync(
                                        imageStream,
                                        textRecognitionMode);

                await GetTextAsync(computerVision, textHeaders.OperationLocation);
            }
        }

        private static async Task GetTextAsync(ComputerVisionClient computerVision, string operationLocation)
        {
            const int numberOfCharsInOperationId = 36;

            // Retrieve the URI where the recognized text will be
            // stored from the Operation-Location header
            string operationId = operationLocation.Substring(operationLocation.Length - numberOfCharsInOperationId);
            TextOperationResult result = await computerVision.GetTextOperationResultAsync(operationId);

            // We can retreive the recognized text only after the TextOperation has been completed.
            int i = 0;
            int maxRetries = 10;
            while ((result.Status == TextOperationStatusCodes.Running || result.Status == TextOperationStatusCodes.NotStarted)
                && i++ < maxRetries)
            {
                Console.WriteLine("Server status: {0}, waiting {1} seconds...", result.Status, i);
                await Task.Delay(1000);

                result = await computerVision.GetTextOperationResultAsync(operationId);
            }

            Console.WriteLine();
            var lines = result.RecognitionResult.Lines;
            foreach (Line line in lines)
            {
                Console.WriteLine(line.Text);
            }
        }
    }
}
