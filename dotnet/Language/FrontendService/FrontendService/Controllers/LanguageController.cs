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
        // The hostname and port of the Kubernetes service which exposes the cognitive service container(s)
        //
        // Instead of hardcoding the hostname and port, environmental variables can be used to discover this address at runtime: 
        // https://kubernetes.io/docs/concepts/containers/container-environment-variables/#cluster-information
        public static string ServiceEndpoint = "http://language:5000";

        // The Client SDK which can be used to make requests to either your self-hosted cognitive service container or the cloud api.
        private ITextAnalyticsClient _taClient;

        public LanguageController()
        {
            _taClient = new TextAnalyticsClient(new MockCredentials())
            {
                // Ensure the sdk client makes requests to your cognitive service containers instead of the cloud API
                Endpoint = ServiceEndpoint
            };
        }

        /// <summary>
        /// A simple health endpoint used for availability and readiness monitoring in Kubernetes.
        /// </summary>
        /// <returns>Always returns "healthy" if the app is able.</returns>
        public string Get()
        {
            return "healthy, cognitive service container endpoint = " + ServiceEndpoint;
        }

        /// <summary>
        /// Detects the language of text retrieved from the HTTP route.!--
        /// Example:  http://<ip>:<port>/Hello World!
        /// Response: English
        /// </summary>
        [HttpGet("{text}")]
        public async Task<string> DetectLanguage([FromRoute] string text)
        {
            try{
                var inputs = new List<Input>() { new Input("id", text) };
                var result = await _taClient.DetectLanguageAsync(new BatchInput(inputs));
                return result.Documents[0].DetectedLanguages[0].Name;
            } catch(Exception ex){
                // returns error is cognitive service container can't be reached
                return ex.Message;
            }
        }
    }
}
