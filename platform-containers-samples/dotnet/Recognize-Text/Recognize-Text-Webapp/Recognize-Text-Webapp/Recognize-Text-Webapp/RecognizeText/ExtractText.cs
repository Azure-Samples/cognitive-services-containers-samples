using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Recognize_Text_Webapp.RecognizeText
{
    public class ExtractText
    {
        public static async Task<String> ExtractLocalTextSync(string endpoint, Stream image)
        {
            using (var imageContent = new StreamContent(image))
            using (var client = new HttpClient())
            {
                String responseString = string.Empty;
                client.BaseAddress = new Uri(endpoint);
                var requestAddress = "/vision/v2.0/recognizetextDirect";
                imageContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

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
