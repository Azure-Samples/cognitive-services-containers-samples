# Installing Language Understanding as a Kubernetes App with Azure

These instructions will guide you through building and deploying a simple Kubernetes sample app backed by a Language Understanding Service container backend. If you want a more simple example of how to run and test the Cognitive Services locally, see our guide to [Running Cognitive Service containers](https://azure.microsoft.com/en-us/blog/running-cognitive-service-containers/).

# Provisioning a Kubernetes cluster

The Cognitive Services Containers can be run in any internet and container-enabled environment, but for this example we'll show how the Azure Kubernetes Service can be used to quickly get a cognitive-services enabled app up and running.

See the [Azure Kubernetes Service (AKS) ](https://docs.microsoft.com/en-us/azure/aks/) documentation for more information about setting up a cluster. Once you have a cluster available and [kubectl](https://kubernetes.io/docs/tasks/tools/install-kubectl/) configured to access the cluster from a dev machine, continue with provisioning a Persistent Storage Solution.

# Provisioning File Storage

Unlike some of the other Cognitive Services Containers, the Language Understanding service allows you to use a custom model so that the service detects intents that are specific to your needs.

The service container expects these models to be mounted as volumes such that they can be loaded at runtime. As such, the container model is flexible enough to support any type of volume, such as the full suite of [Kubernetes Persistent Volumes](https://kubernetes.io/docs/concepts/storage/persistent-volumes/). 

In this example, we'll use a statically created [Azure Files](https://docs.microsoft.com/en-us/azure/storage/files/storage-files-introduction) as the backing storage solutio for the Persistent Volume.

Provision an [Azure File Share](https://docs.microsoft.com/en-us/azure/aks/azure-files-volume#create-an-azure-file-share) and [create a Kubernetes Secret](https://docs.microsoft.com/en-us/azure/aks/azure-files-volume#create-a-kubernetes-secret) so that the containers may mount the store as a volume. 
> Note the secret name and azure file share name, as you'll need to enter them into the yaml app file.

Upload your Language Understanding app file into this storage in the root directory so that it will be available when the container mounts the storage source as a volume.

# Build and Host the Frontend Application

In order to build our frontend app, run docker build on the included Dockerfile to produce an image and then upload that image to your preferred container registry. This sample shows how a registry which requires authentication, such as [Azure Container Registry](https://azure.microsoft.com/en-us/services/container-registry/), can be used. 

In order for this image to be pulled from a private registry into your cluster, make sure you [Create a Secret in the cluster that holds your authorization token](https://kubernetes.io/docs/tasks/configure-pod-container/pull-image-private-registry/#create-a-secret-in-the-cluster-that-holds-your-authorization-token). 
> Note the name of this secret, as you'll need to enter it into the yaml app file.

# Configuring the Helm App

Having provisioned and configured a Kubernetes cluster, Persistent Volume, and frontend image, we're ready to deploy the app.

Edit the `values.yaml` and replace all comments with the relevant values. If it is unclear how to populate these values, such as the billing endpoint and apikey, see our guide to [Running Cognitive Service containers](https://azure.microsoft.com/en-us/blog/running-cognitive-service-containers/)

# Deploying the Application

With [Helm](https://docs.helm.sh/using_helm/), the application can be deployed in one command
```helm
helm install -n language-understanding .\demo\
```

If all values have been provided, this will display a summary of the cluster deployment. You can follow the deployment of pods with `kubectl get pods --watch`. This sample will also create services to expose the app over a public IP. In order to see when these IPs have been exposed as quickly as possible, you may use `kubectl get services --watch`.

# Trying it Out

Once the helm app has been deployed and the relevant pods and services are ready, it's time to test out the app!

Run `kk get services language-understanding-frontend` to see the `EXTERNAL-IP` of the Language Understanding frontend service. 

Point your browser, or other preferred HTTP request generator, to this IP and make a request like `http://<ip-address>/Book a flight to Cairo/` and the frontend will return the determined intent of your utterance, such as `"Book a journey"`!
