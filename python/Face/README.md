# Face Python Samples

[Face](https://azure.microsoft.com/services/cognitive-services/face) can detect human faces in images as well as identifying attributes, including face landmarks (nose, eyes, etc.), gender, age, and other machine-predicted facial features. In addition to detection, Face can check to see if two people in an image or images are the same by using a confidence score, or compare it against a database to see if a similar-looking or identical face already exists. It can also organize similar faces into groups using shared visual traits.

**Note:** The Face cognitive service container is available as a gated preview only. To sign up for the gated preview, please submit a [Cognitive Services Vision Containers Request](http://aka.ms/VisionContainersPreview).

## Prerequisites

These samples assume that you have the following pieces of software installed:

* [Docker](https://www.docker.com/products/docker-desktop) (any platform is fine)
* [Python](hhttps://www.python.org/) (any platform is fine)
* Access to the gated preview

## Getting started

1. Launch the Face docker container. The ApiKey and Billing parameters can be obtained from an already exiting Face Cognitive Service in the [Azure Portal](https://portal.azure.com). See the [How-to](https://go.microsoft.com/fwlink/?linkid=2018836&clcid=0x409) and [configuration documentation](https://go.microsoft.com/fwlink/?linkid=2018900&clcid=0x409) for how to use and configure the container image.

```
docker run --rm -it -p 5000:5000 <repository>/microsoft/cognitive-services-face eula=accept ApiKey=<api key> Billing=<endpoint>
```

2. Verify that the container is running by pointing your browser at `http://localhost:5000`.

1. Browse and try out the container directly by going to `http://localhost:5000/swagger`.

## Face Using Basic HTTP

Demonstrates how to use basic HTTP to interact with a [Cognitive Services keyphrase container](http://aka.ms/cognitive-services-containers).

Run the sample directly from the command line by typing

```
cd FaceHttp
python FaceHttp.py
```
