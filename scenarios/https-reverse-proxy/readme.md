[[_TOC_]]

# HTTPS
By default, Cognitive Services containers do not expose HTTPS endpoints because certificates must be supplied on a per-application basis. In order to enable HTTPS in your environment, we recommend using a high-quality open-source solution with proxying features such as **Nginx** and **Istio**.

## HTTPS for a single container with Nginx
[Nginx](https://www.nginx.com/resources/wiki/) is an open-source, high-performance HTTP server and proxy. An Nginx container can be used as a sidecar to terminate a TLS connection for a single container. A more complex Nginx Ingress-based TLS termination solution can also be constructed.

### Creating a Config
Nginx uses [configuration files](https://docs.nginx.com/nginx/admin-guide/basic-functionality/managing-configuration-files/) to enable features at runtime. In order to enable TLS termination for another service, we must specify an `ssl_certificate` to terminate the TLS connection and a `proxy_pass` to specify an address for the service.
> Note: `ssl_certificate` expects a path to be specified within the Nginx container's local filesystem
> Note: The address specified for `proxy_pass` must be available from within the Nginx container's network.
```conf
server {
  listen              80;
  return 301 https://$host$request_uri;
}

server {
  listen              443 ssl;
  ssl_certificate     /cert/cognitive-services-TLS-test.crt;
  ssl_certificate_key /cert/cognitive-services-TLS-test.key;

  location / {
    proxy_pass http://cognitive-service:5000;
    proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
    proxy_set_header X-Real-IP  $remote_addr;
  }
}
```

### Mounting the Config
The [Nginx container](https://hub.docker.com/_/nginx) will load all `.conf` files mounted under `/etc/nginx/conf.d/` into the `http` configuration path.

### Docker-Compose Example
For a sample docker-compose file, see [docker-compose.yml](docker-compose.yml)

### Kubeconfig Example
For an all-in-one example for Kubernetes, see [kubeconfig.yml](kubeconfig.yml)

## HTTPS for all containers with Istio
[Istio](https://istio.io/docs/concepts/what-is-istio/) is an open platform to connect, monitor, and secure microservices. For details about Istio outside of the scope of this example, please see the [Istio documentation](https://istio.io/docs/). Istio uses [Envoy](https://blog.getambassador.io/envoy-vs-nginx-vs-haproxy-why-the-open-source-ambassador-api-gateway-chose-envoy-23826aed79ef) as a proxy in order to enable HTTPS in a Kubernetes cluster.

### Installing Istio
Istio must be [installed](https://istio.io/docs/setup/getting-started/) within a cluster before it can be used to enable HTTPS. This can be done with either [Istioctl](https://istio.io/docs/setup/install/istioctl/) (akin to kubectl) or [Helm](https://istio.io/docs/setup/install/helm/). For more details on installing Istio, see the [Getting Started](https://istio.io/docs/setup/getting-started/) guide.

### Configuring HTTPS
In order to configure HTTPS, you will need to [configure](https://istio.io/docs/tasks/traffic-management/ingress/ingress-control/#configuring-ingress-using-an-istio-gateway) a [Gateway](https://istio.io/docs/reference/config/networking/gateway/) and provide a custom certificate.

### Additional Details

Istio can be configured to [automatically inject](https://istio.io/docs/ops/configuration/mesh/injection-concepts/) a sidecar container for each container, automatically enabling HTTPS and other features for all services within your cluster.

Please see the [Networking documentation](https://istio.io/docs/reference/config/networking/) for more details.
