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
        // The hostname and port of the Kubernetes service which exposes the cognitive service container(s)
        //
        // Instead of hardcoding the hostname and port, environmental variables can be used to discover this address at runtime: 
        // https://kubernetes.io/docs/concepts/containers/container-environment-variables/#cluster-information
        public static string ServiceEndpoint = "http://language-understanding:5000";

        // The ID of your app/model.
        // For more information about creating a new model see: https://docs.microsoft.com/en-us/azure/cognitive-services/luis/luis-how-to-start-new-app
        private const string APP_NAME = "da63910e-dddf-4c2f-a38f-a250a91ca176";

        // The Client SDK which can be used to make requests to either your self-hosted cognitive service container or the cloud api.
        private ILUISRuntimeClient _luisClient;

        public LanguageUnderstandingController()
        {
            _luisClient = new LUISRuntimeClient(new MockCredentials())
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
            return "healthy";
        }

        /// <summary>
        /// Detects the intent of text retrieved from the HTTP route.
        /// Example:  http://<ip>:<port>/Book a flight to Cairo.
        /// Response: Book a Journey
        /// </summary>
        [HttpGet("{text}")]
        public async Task<string> DetectIntent([FromRoute] string text)
        {
            var result = await _luisClient.Prediction.ResolveAsync(APP_NAME, text);
            return result.TopScoringIntent.Intent;
        }
    }
}
