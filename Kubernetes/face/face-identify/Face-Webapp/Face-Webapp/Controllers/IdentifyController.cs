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
    
    public class IdentifyController : Controller
    {
        // ApiKey is not needed on client side talking to a container
        private const string ApiKey = "00000000000000000000000000000000";
        private const string Endpoint = "http://face:5000";
        private const string GroupId = "group-id";

        [HttpGet("/identify")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("/identify/result")]
        public async Task<IActionResult> Post(List<IFormFile> files)
        {
            string faceData = string.Empty;
            var filePath = Path.GetTempFileName();

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    using (var stream = formFile.OpenReadStream())
                    {
                        var identified = await IdentifyFace(stream);
                        faceData += "\n" + formFile.FileName + ": \n" + identified + "\n";
                    }
                }
            }
            return Ok(faceData);
        }

        private async Task<String> IdentifyFace(Stream image)
        {
            String responseString = string.Empty;

            using (var client = new FaceClient(new ApiKeyServiceClientCredentials(ApiKey)) { Endpoint = Endpoint })
            {
                var attributes = new FaceAttributeType[] { FaceAttributeType.Gender, FaceAttributeType.Age, FaceAttributeType.Smile, FaceAttributeType.Glasses };
                var detectedFaces = await client.Face.DetectWithStreamAsync(image, returnFaceAttributes: attributes);

                if (detectedFaces == null || detectedFaces.Count == 0)
                {
                    responseString = "No faces detected from image.";
                }
                else
                {
                    // IList does not define the Select method
                    var faceIds = new List<Guid>();
                    foreach(var face in detectedFaces)
                    {
                        faceIds.Add(face.FaceId.Value);
                    }

                    var result = await client.Face.IdentifyAsync(faceIds, GroupId);
                    responseString = result.ToString();
                }
            }
            return responseString;
        }

    }
}
