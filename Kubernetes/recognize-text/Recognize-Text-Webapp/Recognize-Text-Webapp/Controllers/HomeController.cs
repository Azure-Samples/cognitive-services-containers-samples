using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Recognize_Text_Webapp.Controllers
{
    public class HomeController : Controller
    {
        private readonly Uri Endpoint = new Uri("http://recognizetext:5000");


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
                        var lines = await ExtractTextSync(stream);
                        recognizedText += "\n" + formFile.FileName + ": \n" + lines;
                    }
                }
            }
            return Ok(recognizedText);
        }

        private async Task<String> ExtractTextSync(Stream image)
        {
            String responseString = string.Empty;

            using (var imageContent = new StreamContent(image))
            using (var client = new HttpClient())
            {
                imageContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                client.BaseAddress = Endpoint;
                var requestAddress = "/vision/v2.0/recognizetextDirect";

                using (var response = await client.PostAsync(requestAddress, imageContent))
                {
                    var resultAsString = await response.Content.ReadAsStringAsync();
                    var resultAsJson = JsonConvert.DeserializeObject<JObject>(resultAsString);

                    if (resultAsJson["lines"] == null)
                    {
                        responseString = resultAsString;
                    }
                    else
                    {
                        foreach (var line in resultAsJson["lines"])
                        {
                            responseString += line["text"] + "\n";
                        }
                    }
                }

                return responseString;
            }
        }

    }
}
