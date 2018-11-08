# FrontendService
A sample web api app using the Language SDK to detect the language of a sentence.

# add-registry-secret.ps1
In order for your Kubernetes Pods to pull from the container registry, you'll need a Kubernetes secret with the registry credentials. Run this script to get help creating that secret from credentials stored in an Azure KeyVault.

# language.yml
Using `kubectl apply -f language.yml`, you can deploy this frontend and container as a Kubernetess app to your cluster.