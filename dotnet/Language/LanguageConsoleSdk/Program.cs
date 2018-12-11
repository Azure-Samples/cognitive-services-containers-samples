using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics.Models;
using Microsoft.Rest;

namespace FaceConsole
{
    class MockCredentials : ServiceClientCredentials { }

    class Program
    {
        // ApiKey is not needed on client side talking to a container
        private const string ApiKey = "00000000000000000000000000000000";
        private const string Endpoint = "http://localhost:5000";

        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Usage : \"<Phrase>\"");
                Environment.Exit(-1);
            }

            var phrase = args[0];

            var client = new TextAnalyticsClient(new MockCredentials()) { Endpoint = Endpoint };
            DetectFaces(client, phrase).Wait();
        }

        private static async Task DetectFaces(TextAnalyticsClient client, string phrase)
        {
            var inputs = new List<Input>() { new Input("id", phrase) };
            var result = await client.DetectLanguageAsync(new BatchInput(inputs));
            string name = result.Documents[0].DetectedLanguages[0].Name;
            Console.WriteLine($"Detected Language: {name}");
        }
    }
}
