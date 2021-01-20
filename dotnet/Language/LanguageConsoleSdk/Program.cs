using Azure;
using Azure.AI.TextAnalytics;
using System;
using System.Threading.Tasks;

namespace FaceConsole
{
    class Program
    {
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
            var client = new TextAnalyticsClient(new Uri(Endpoint), new AzureKeyCredential(ApiKey));
            DetectFaces(client, phrase).Wait();
        }

        private static async Task DetectFaces(TextAnalyticsClient client, string phrase)
        {
            DetectedLanguage language = await client.DetectLanguageAsync(phrase).ConfigureAwait(false);
            Console.WriteLine($"Detected Language: {language.Name}");
        }
    }
}
