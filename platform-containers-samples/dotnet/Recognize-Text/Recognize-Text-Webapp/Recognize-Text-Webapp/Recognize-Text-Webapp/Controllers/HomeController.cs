using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Recognize_Text_Webapp.Models;
using Recognize_Text_Webapp.RecognizeText;

namespace Recognize_Text_Webapp.Controllers
{
    public class HomeController : Controller
    {
        private readonly string Endpoint;

        public HomeController(IConfiguration config)
        {
            Endpoint = "http://recognizetext:5000";
        }

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
                        var lines = await ExtractText.ExtractLocalTextSync(Endpoint, stream);

                        recognizedText += "\n" + formFile.FileName + ": \n" + lines;
                    }
                }
            }
            return Ok(recognizedText);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
