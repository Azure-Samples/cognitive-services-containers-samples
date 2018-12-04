## FrontendService
A sample web API app using the Language SDK, [Text Analytics](https://docs.microsoft.com/en-us/azure/cognitive-services/text-analytics/) to detect the natural language of a sentence.

## add-registry-secret.ps1
In order for your Kubernetes pods to pull from the container registry, you'll need a Kubernetes secret with the registry credentials. Run this script to get help creating that secret from credentials stored in an Azure KeyVault.

## language.yml
Using `kubectl apply -f language.yml`, you can deploy this frontend and container as a Kubernetess app to your cluster.

## Resources

[Nuget package for Text Analytics](https://www.nuget.org/packages/Microsoft.Azure.CognitiveServices.Language.TextAnalytics)
