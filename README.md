# Container support for Azure Cognitive Services

Container support in [Azure Cognitive Services](https://docs.microsoft.com/en-us/azure/cognitive-services/) allows developers to use the same rich APIs that are available in Azure, but with the flexibility that comes with [Docker containers](https://www.docker.com/what-container). Container support is currently available in preview for a subset of Azure Cognitive Services, including parts of [Computer Vision](https://review.docs.microsoft.com/en-us/azure/cognitive-services/computer-vision/home), [Face](https://review.docs.microsoft.com/en-us/azure/cognitive-services/face/overview), and [Text Analytics](https://review.docs.microsoft.com/en-us/azure/cognitive-services/text-analytics/overview).

Containerization is an approach to software distribution in which an application or service, including its dependencies & configuration, is packaged together as a container image. With little or no modification, a container image can be deployed on a container host, such as Microsoft Server, Amazon Web Services, or a private server. Containers are isolated from each other and the underlying operating system, with a smaller footprint than a virtual machine. Containers can be instantiated from container images for short-term tasks and removed when no longer needed. 

## Features and benefits

* <b>Control over data</b>: Allows customers to use Cognitive Services with complete control over their data. This is essential for customers that cannot send data to the cloud but need access to Cognitive Services technology. Supports consistency in hybrid environments – across data, management, identity, and security.
* <b>Control over model updates</b>: Provides customers flexibility in versioning and updating of models deployed in their solutions.
* <b>Portable architecture</b>: Enables the creation of a portable application architecture that can be deployed in the cloud, on-premises and the edge. Containers can also be deployed directly to [Azure Kubernetes Service](https://review.docs.microsoft.com/en-us/azure/aks/), [Azure Container Instances](https://review.docs.microsoft.com/en-us/azure/container-instances/), or to a [Kubernetes](https://kubernetes.io/) cluster deployed to [Azure Stack](https://review.docs.microsoft.com/en-us/azure/azure-stack/). For more information, see [Deploy Kubernetes to Azure Stack](https://review.docs.microsoft.com/en-us/azure/azure-stack/user/azure-stack-solution-template-kubernetes-deploy).
* <b>High throughput / low latency</b>: Provides customers the ability to scale for high throughput and low latency requirements by enabling Cognitive Services to run in Azure Kubernetes Service physically close to their application logic and data.

## Prerequisites

You must satisfy the following prerequisites before using Azure Cognitive Services containers:

<b>Docker Engine</b>: Install Docker Engine locally. Configure your Docker environment with Docker packages for [macOS](https://docs.docker.com/docker-for-mac/), [Linux](https://docs.docker.com/engine/installation/#supported-platforms), or [Windows](https://docs.docker.com/docker-for-windows/). On Windows, Docker must be configured to support Linux containers. Docker containers can also be deployed directly to [Azure Kubernetes Service](https://review.docs.microsoft.com/en-us/azure/aks/) or [Azure Container Instances](https://review.docs.microsoft.com/en-us/azure/container-instances/).

Docker must be configured to allow the containers to connect with and send billing data to Azure.

<b>Familiarity with [Microsoft Container Registry](https://docs.microsoft.com/en-us/azure/container-registry/) and Docker</b>: concepts, like registries, repositories, containers, and container images, as well as knowledge of basic docker commands. 

For a primer on Docker and container basics, see the [Docker overview](https://docs.docker.com/engine/docker-overview/).

Individual containers can have their own requirements, as well, including server and memory allocation requirements.

## Installation / run

Each sample has its own installation and run instructions in their corresponding Readme.

## Quickstarts

- [Quickstart: Create a container registry using the Azure portal](https://docs.microsoft.com/en-us/azure/container-registry/container-registry-get-started-portal)
- [Quickstart: Create a container registry using the Azure CLI](https://docs.microsoft.com/en-us/azure/container-registry/container-registry-get-started-azure-cli)
- [Quickstart: Create an Azure Container Registry using PowerShell](https://docs.microsoft.com/en-us/azure/container-registry/container-registry-get-started-powershell)

## Resources

- [Docker Documentation](https://docs.docker.com/)
- [Azure Container Registry Documentation](https://docs.microsoft.com/en-us/azure/container-registry/)
- [Container support in Azure Cognitive Services](https://review.docs.microsoft.com/en-us/azure/cognitive-services/cognitive-services-container-support?branch=release-cogserv-containers-support)
- [Azure Cognitive Services Containers](https://github.com/Azure/CSContainers) - integrating Cognitive Services with Containers
