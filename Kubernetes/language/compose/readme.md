# Docker Compose with Kubernetes

## Ensure that Compose on Kubernetes is installed

Check that Compose on Kubernetes is installed by checking for the availability of the API using the command:
```bash
$ kubectl api-versions | grep compose
compose.docker.com/v1beta1
compose.docker.com/v1beta2
```

> If Compose on Kubernetes is not installed, follow instructions for [Deploying Compose on Kubernetes](https://github.com/docker/compose-on-kubernetes#deploying-compose-on-kubernetes).

## Install the Stack to Your Cluster

First, Update the `command` item in `docker-compose.yml` to include your valid `billing` endpoint and `apikey`.

Then, the app can be deployed with

```bash
$ docker stack deploy --orchestrator=kubernetes -c docker-compose.yml language
```

## Try It

Verify that your containers are `Running` and that the `EXTERNAL-IP`s of your services have been resolved. (This may take a few minutes)

```bash
$ kubectl get all
...
po/language-64b9459d4d-qgtjc            1/1       Running   0          2m
po/language-frontend-7d546b4d4b-nwz2v   1/1       Running   0          2m
...
svc/language-frontend-published   10.0.245.118   13.92.143.10    80:31246/TCP     2m
svc/language-published            10.0.239.148   13.92.138.224   5000:30893/TCP   2m
...
```

Make a web request to the `EXTERNAL-IP` of your language-frontend-published service
```bash
$ curl -s 13.92.143.10/Hola
Spanish
```

## Remove the Stack from Your Cluster
```bash
kubectl delete stack language
```