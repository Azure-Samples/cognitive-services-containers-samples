## Simple instructions <br>
(instructions reduce down to this if you keep secret names, etc, static)

1. Build and upload the frontend project image
1. Add registry secrets to the cluster using create-secrets.ps1
1. Run `helm install -n <name> <dir> --set $(az keyvault secret show --vault-name <vault-name> --name <name> --query value)` to launch the kubernetes app

## Customized instructions

1. Build the frontend docker image
1. Push the frontend docker image to an ACR
1. Specify this image address in values.yaml under Frontend:Image
1. Specify which CS container to use in values.yaml Language:Image
1. Run `helm install -n language . --set Configuration.KeyVault.Secret=<secret>` to launch the kubernetes app.
1. Run `helm upgrade language . --set Configuration.KeyVault.Secret=<secret>` to update the app after making changes to a yaml file.

## Investigation/Observation

- `kubectl get all`
- `kubectl logs <pod> --follow`
- `kubectl get services --watch`
- `kubectl proxy` (then open a web browser to `http://localhost:8001/api/v1/namespaces/kube-system/services/kubernetes-dashboard/proxy/#!/overview?namespace=default`)
