# Keyphrase Python Samples

KeyPhrase is part of [Text Analytics](https://azure.microsoft.com/services/cognitive-services/text-analytics) and can extract the key talking points and highlights in text in either English, German, Spanish, or Japanese.

## Prerequisites

These samples assume that you have the following pieces of software installed:

* [Docker](https://www.docker.com/products/docker-desktop) (any platform is fine)
* [Python](https://www.python.org/) (any platform is fine)

## Getting started

1. Launch the Keyphrase docker container. The ApiKey and Billing parameters can be obtained from an already exiting Text Analytics Cognitive Service in the [Azure Portal](https://portal.azure.com). See the [How-to](https://go.microsoft.com/fwlink/?linkid=2018654&clcid=0x409) and [configuration documentation](https://go.microsoft.com/fwlink/?linkid=2018592&clcid=0x409) for how to use and configure the container image.

```
docker run --rm -it -p 5000:5000 mcr.microsoft.com/azure-cognitive-services/keyphrase eula=accept ApiKey=<api key> Billing=<endpoint>
```

2. Verify that the container is running by pointing your browser at `http://localhost:5000`.

1. Browse and try out the container directly by going to `http://localhost:5000/swagger`.

## Keyphrase Using Basic Http

Demonstrates how to use basic HTTP to interact with a [Cognitive Services keyphrase container](http://aka.ms/cognitive-services-containers).

Run the sample directly from the command line by typing

```
cd SentimentHttp
python SentimentHttp.py
```
