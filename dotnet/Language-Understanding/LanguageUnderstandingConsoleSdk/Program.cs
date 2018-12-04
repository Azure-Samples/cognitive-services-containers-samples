using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Language.LUIS.Runtime;

namespace LanguageUnderstandingConsole
{
    class Program
    {
        private const string Endpoint = "http://localhost:5000";
        private const string ApplicationID = "da63910e-dddf-4c2f-a38f-a250a91ca176";

        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Usage : \"<Phrase>\"");
                Environment.Exit(-1);
            }

            var query = args[0];

            var client = new LUISRuntimeClient(new ApiKeyServiceClientCredentials("None")) { Endpoint = Endpoint };
            DetectIntent(client, query).Wait();
        }

        private static async Task DetectIntent(LUISRuntimeClient client, string query)
        {
            var prediction = await client.Prediction.ResolveAsync(ApplicationID, query);
            Console.WriteLine($"Top Intent: {prediction.TopScoringIntent.Intent}");
            Console.WriteLine();
            Console.WriteLine($"Entities");
            prediction.Entities.ToList().ForEach(e => Console.WriteLine($"   {e.Entity}"));
        }
    }
}
