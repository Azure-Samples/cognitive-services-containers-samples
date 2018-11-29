# Recognize Text

Simple Kubernetes cluster with a frontend application that interfaces with the recognize text container.

## Getting started

1. Build and upload the front end image : `Face-Webapp` to the container registry.
2. Set the frontend image value in `face-identify.yml`.
3. In `face-identify.yml` under `imagePullSecrets`, replace `frontendregcred` with credentials created for your container registry. [More information on pulling from a private registry](https://kubernetes.io/docs/tasks/configure-pod-container/pull-image-private-registry/)

4. Create registry credentials for the Container Preview registry, as detailed [here](https://thorsten-hans.com/how-to-use-a-private-azure-container-registry-with-kubernetes-9b86e67b93b6). Replace `regcred` in `imagePullSecrets` with these credentials.

5. Run `kubectl create -f .\face-identify.yml` to create the kubernetes services for the frontend and cognitive services container.
6. You can now connect to `face-frontend` to interact with the frontend service. 
    - Since `minikube` does not always support external IP addresses, you can get a public address for this service by running `minikube service face-frontend --url`.

