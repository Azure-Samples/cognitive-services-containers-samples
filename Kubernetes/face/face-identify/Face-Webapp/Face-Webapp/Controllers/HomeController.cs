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
        public static string ApiKey = "00000000000000000000000000000000";
        public static string Endpoint = "http://face:5000";
        public static string GroupId = "mypersongroupid";
        public static string GroupName = "mypersongroupname";

        private FaceClient _client = null;

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("UploadFiles")]
        public async Task<IActionResult> Post(List<IFormFile> files)
        {
            await CreateGroup();
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    using (var stream = formFile.OpenReadStream())
                    {
                        await AddToGroup(stream);
                    }
                }
            }

            await _client.PersonGroup.TrainAsync(GroupId);
            return View("~/Views/Identify/Identify.cshtml");
        }

        private async Task AddToGroup(Stream image)
        {
            var personName = Guid.NewGuid().ToString();
            var person = await _client.PersonGroupPerson.CreateAsync(GroupId, personName);
            await _client.PersonGroupPerson.AddFaceFromStreamAsync(GroupId, person.PersonId, image);
        }

        private async Task CreateGroup()
        {
            _client = new FaceClient(new ApiKeyServiceClientCredentials(ApiKey)) { Endpoint = Endpoint };
            await _client.PersonGroup.CreateAsync(GroupId, GroupName);
        }
    }
}
