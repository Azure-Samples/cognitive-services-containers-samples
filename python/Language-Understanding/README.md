# Language Understanding Python Samples

[Language Understanding](https://azure.microsoft.com/en-us/services/cognitive-services/language-understanding-intelligent-service/) applies custom intelligence to conversational, natural language text to predict overall meaning, and pull out relevant, detailed information.

## Prerequisites

These samples assume that you have the following pieces of software installed:

* [Docker](https://www.docker.com/products/docker-desktop) (any platform is fine)
* [Python](https://www.python.org/) (any platform is fine)

## Getting started

1. Launch the Language Understanding docker container. The ApiKey and Billing parameters can be obtained from an already exiting Text Analytics Cognitive Service in the [Azure Portal](https://portal.azure.com). See the [How-to](https://go.microsoft.com/fwlink/?linkid=2018654&clcid=0x409) and [configuration documentation](https://go.microsoft.com/fwlink/?linkid=2018592&clcid=0x409) for how to use and configure the container image.

```
docker run --rm -it -v <models directory>:/input -p 5000:5000 mcr.microsoft.com/azure-cognitive-services/luis eula=accept ApiKey=<api key> Billing=<endpoint>
```

2. Verify that the container is running by pointing your browser at `http://localhost:5000`.

1. Browse and try out the container directly by going to `http://localhost:5000/swagger`.

## Language Understanding Using Basic Http

Demonstrates how to use basic HTTP to interact with a [Cognitive Services Language Understanding container](http://aka.ms/cognitive-services-containers).

Run the sample directly from the command line by typing

```
cd Language-UnderstandingHttp
python Language-UnderstandingHttp.py
```
