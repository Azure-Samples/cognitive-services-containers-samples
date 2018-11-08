#Recognize Text

Simple kubernetets cluster with a frontend application that interfaces with the recognize text container.

## Getting started

1. Build and upload the front end image : `Platform-Containers-Samples\dotnet\Recognize-Text\Recognize-Text-Webapp\Recognize-Text-Webapp` to container registry.
2. Set `recognizetext-frontend:image` to the name of the front end image in `recognizetext.yml`
3. In `recognizetext.yml` under `imagePullSecrets`, replace `regcredbrwals` with credentials created for your container registry. [More information on pulling from a private registry](https://kubernetes.io/docs/tasks/configure-pod-container/pull-image-private-registry/)

4. Create registry credentials for the Container Preview registry, as detailed [here](https://thorsten-hans.com/how-to-use-a-private-azure-container-registry-with-kubernetes-9b86e67b93b6). Replace `regcred` in `imagePullSecrets` with these credentials.

5. Run `kubectl create -f .\recognizetext.yml` to create the kubernetes services for the frontend and cognitive services container.
6. You can now connect to `recognizetext-frontend` to interact with the frontend service.
    - Since `minikube` does not always support external IP addresses, you can get a public address for this service by running `minikube service recognizetext-frontend --url`


## Investigation/Observation

- `kubectl get all`
- `kubectl get pods`
- `kubectl logs <pod> --follow`
- `kubectl get services --watch`