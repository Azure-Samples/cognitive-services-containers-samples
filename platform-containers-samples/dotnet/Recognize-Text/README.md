# Recognize Text
 Sample Console Apps that demonstrates the optical character recognition capabilities of Recognize Text.
 
 [Recognizing printed and handwritten text](https://docs.microsoft.com/en-us/azure/cognitive-services/computer-vision/concept-recognizing-text)

## Recognize-Text-Async

Demonstrates how the `CognitiveServices.Vision.ComputerVision` SDK can be used to interact with a running Recognize-Text container using an async interface. Outputs the text recognized from an image to the console.

Usage:

`dotnet run .\Recognize-Text-Async.csproj  <Container URI> <SubscriptionKey> <Image path>`


## Recognize-Text-Direct

Demonstrates how a HTTP Client can be used to directly interact with a running Recognize-Text container using a direct endpoint. Outputs the text recognized from an image to the console.

Usage:

`dotnet run .\Recognize-Text-Direct.csproj  <Container URI> <Image path>`

