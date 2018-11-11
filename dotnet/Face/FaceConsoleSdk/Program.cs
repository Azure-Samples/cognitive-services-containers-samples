using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;

namespace FaceConsole
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
                Console.WriteLine("  Image: Local file path for image containing one or more faces.");
                Environment.Exit(-1);
            }

            var image = args[1];
            if (!File.Exists(image))
            {
                Console.WriteLine($"Unable to open or read '{image}'");
                Environment.Exit(-1);
            }

            var client = new FaceClient(new ApiKeyServiceClientCredentials(ApiKey)) { Endpoint = Endpoint };
            DetectFaces(client, image).Wait();
        }

        private static async Task DetectFaces(FaceClient client, string image)
        {
            using (var imageStream = File.OpenRead(image))
            {
                var attributes = new FaceAttributeType[] { FaceAttributeType.Gender, FaceAttributeType.Age, FaceAttributeType.Smile, FaceAttributeType.Glasses };
                var detectedFaces = await client.Face.DetectWithStreamAsync(imageStream, returnFaceAttributes: attributes);

                if (detectedFaces?.Count == 0)
                {
                    Console.Write($"No faces detected from image `{image}`.");
                }
                else
                {
                    foreach (var face in detectedFaces)
                    {
                        var rect = face.FaceRectangle;
                        Console.WriteLine($"Rectangle: {rect.Left} {rect.Top} {rect.Width} {rect.Height}");
                        Console.WriteLine($"Gender: {face.FaceAttributes.Gender}");
                        Console.WriteLine($"Age: {face.FaceAttributes.Age}");
                        Console.WriteLine($"Smile: {face.FaceAttributes.Smile}");
                        Console.WriteLine($"Glasses: {face.FaceAttributes.Glasses}");
                        Console.WriteLine("");
                    }
                }
            }
        }
    }
}
