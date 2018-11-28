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
        private const string GroupId = "group-id";

        private FaceClient _client;

        public HomeController()
        {
            _client = new FaceClient(new ApiKeyServiceClientCredentials(ApiKey)) { Endpoint = Endpoint };
            Task.WaitAll(CreateGroup());
        }

        ~HomeController()
        {
            _client.Dispose();
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("UploadFiles")]
        public async Task<IActionResult> Post(List<IFormFile> files)
        {
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

            await TrainGroup();

            return Ok("Training complete.");
        }

        private async Task AddToGroup(Stream image)
        {
            var person = await _client.PersonGroupPerson.CreateAsync(GroupId);
            await _client.PersonGroupPerson.AddFaceFromStreamAsync(GroupId, person.PersonId, image);
        }

        private async Task CreateGroup()
        {
            await _client.PersonGroup.CreateAsync(GroupId);
        }

        private async Task TrainGroup()
        {
            await _client.PersonGroup.TrainAsync(GroupId);
        }

    }
}
