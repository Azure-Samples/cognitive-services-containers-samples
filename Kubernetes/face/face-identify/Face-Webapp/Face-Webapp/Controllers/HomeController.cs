using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Face_Webapp.Controllers
{
    public class HomeController : Controller
    {
        // ApiKey is not needed on client side talking to a container
        private const string ApiKey = "00000000000000000000000000000000";
        private const string Endpoint = "http://face:5000";

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("UploadFiles")]
        public async Task<IActionResult> Post(List<IFormFile> files)
        {
            string recognizedText = string.Empty;
            var filePath = Path.GetTempFileName();

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    using (var stream = formFile.OpenReadStream())
                    {
                        var lines = await ExtractFace(stream);
                        recognizedText += "\n" + formFile.FileName + ": \n" + lines;
                    }
                }
            }
            return Ok(recognizedText);
        }

        private async Task<String> ExtractFace(Stream image)
        {
            String responseString = string.Empty;

            using (var client = new FaceClient(new ApiKeyServiceClientCredentials(ApiKey)) { Endpoint = Endpoint })
            {
                var attributes = new FaceAttributeType[] { FaceAttributeType.Gender, FaceAttributeType.Age, FaceAttributeType.Smile, FaceAttributeType.Glasses };
                var detectedFaces = await client.Face.DetectWithStreamAsync(image, returnFaceAttributes: attributes);

                if (detectedFaces?.Count == 0)
                {
                    responseString = ">No faces detected from image.";
                }
                else
                {
                    foreach (var face in detectedFaces)
                    {
                        var rect = face.FaceRectangle;
                        responseString = $">Rectangle: {rect.Left} {rect.Top} {rect.Width} {rect.Height}\n";
                        responseString += $">Gender: {face.FaceAttributes.Gender}\n";
                        responseString += $">Age: {face.FaceAttributes.Age}\n";
                        responseString += $">Smile: {face.FaceAttributes.Smile}\n";
                        responseString += $">Glasses: {face.FaceAttributes.Glasses}\n";
                    }
                }
            }
            return responseString;
        }

    }
}
