using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics.Models;
using Microsoft.Rest;

namespace FrontendService.Controllers
{
    public class MockCredentials : ServiceClientCredentials {}

    [Route("/")]
    public class LanguageController : Controller
    {
        public static string ServiceEndpoint = "http://language:5000";
        private ITextAnalyticsClient _taClient;

        public LanguageController()
        {
            _taClient = new TextAnalyticsClient(new MockCredentials())
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
            var inputs = new List<Input>() { new Input("id", text) };
            var result = await _taClient.DetectLanguageAsync(new BatchInput(inputs));
            return result.Documents[0].DetectedLanguages[0].Name;
        }
    }
}
