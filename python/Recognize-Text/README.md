# Recognize-Text Python Samples

Recognize-Text is part of [Computer Vision](https://azure.microsoft.com/services/cognitive-services/computer-vision) and can detect text in an image using optical character recognition (OCR) and extract the recognized words into a machine-readable character stream. Analyze images to detect embedded text, generate character streams, and enable searching. Save time and effort by taking photos of text instead of copying it.

**Note:** The Recognize-Text cognitive service container is available as a gated preview only. To sign up for the gated preview, please submit a [Cognitive Services Vision Containers Request](http://aka.ms/VisionContainersPreview).

## Prerequisites

These samples assume that you have the following pieces of software installed:

* [Docker](https://www.docker.com/products/docker-desktop) (any platform is fine)
* [Python](https://www.python.org/) (any platform is fine)
* Access to the gated preview

## Getting started

1. Launch the Recognize-Text docker container. The ApiKey and Billing parameters can be obtained from an already exiting Computer Vision Cognitive Service in the [Azure Portal](https://portal.azure.com). See the [How-to](https://go.microsoft.com/fwlink/?linkid=2018848&clcid=0x409) and [configuration documentation](https://go.microsoft.com/fwlink/?linkid=2018904&clcid=0x409) for how to use and configure the container image.

```
docker run --rm -it -p 5000:5000 <repository>/microsoft/cognitive-services-recognize-text eula=accept ApiKey=<api key> Billing=<endpoint>
```

2. Verify that the container is running by pointing your browser at `http://localhost:5000`.

1. Browse and try out the container directly by going to `http://localhost:5000/swagger`.

## Recognize-Text Using Basic Http

Demonstrates how to use basic HTTP to interact with a [Cognitive Services Recognize-Text container](http://aka.ms/cognitive-services-containers).

Run the sample directly from the command line by typing

```
cd Recognize-TextHttp
python Recognize-TextHttp.py
```
