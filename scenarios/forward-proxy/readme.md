# Running Cognitive Services containers Behind a Proxy
Many corporate environments require a proxy to safeguard against exfiltration of sensitive data from a network.

Cognitive Services containers must maintain intermittent connectivity to report metered usage, and they can be configured to do so via a proxy using the variables `HTTP_PROXY` and `HTTP_PROXY_CREDS`.

See [kubeconfig.yml](kubeconfig.yml) for an example of how to configure this in Kubernetes, or [docker-compose.yml](docker-compose.yml).
