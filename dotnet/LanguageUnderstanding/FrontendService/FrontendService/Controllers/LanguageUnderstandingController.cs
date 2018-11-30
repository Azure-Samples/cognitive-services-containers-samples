using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.CognitiveServices.Language.LUIS.Runtime;
using Microsoft.Rest;

namespace FrontendService.Controllers
{
    public class MockCredentials : ServiceClientCredentials {}

    [Route("/")]
    public class LanguageUnderstandingController : Controller
    {
        public static string ServiceEndpoint = "http://language-understanding:5000";
        private const string APP_NAME = "da63910e-dddf-4c2f-a38f-a250a91ca176";
        private ILUISRuntimeClient _luisClient;

        public LanguageUnderstandingController()
        {
            _luisClient = new LUISRuntimeClient(new MockCredentials())
            {
                Endpoint = ServiceEndpoint
            };
        }

        public string Get()
        {
            return "healthy";
        }

        // POST api/values
        [HttpGet("{text}")]
        public async Task<string> DetectLanguage([FromRoute] string text)
        {
            var result = await _luisClient.Prediction.ResolveAsync(APP_NAME, text);
            return result.TopScoringIntent.Intent;
        }
    }
}
