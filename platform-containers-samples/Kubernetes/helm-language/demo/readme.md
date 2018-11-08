Simple instructions (instructions reduce down to this if you keep secret names, etc, static)

1) Build and upload the frontend project image
2) Add registry secrets to the cluster using create-secrets.ps1
3) Run `helm install -n <name> <dir> --set $(az keyvault secret show --vault-name brwals-keys --name cscp-demo-token --query value)` to launch the kubernetes app

Customized instructions

1)  Build the frontend docker image
2)  Push the frontend docker image to an ACR
3)  Specify this image address in values.yaml under Frontend:Image
5)  Specify which CS container to use in values.yaml Language:Image
6)  If you used ACRs other than brwalsincubationregistry and containerpreview, update create-secrets.ps1 and the "RegistrySecret" values in values.yaml
7)  Run `helm install -n language . --set Configuration.KeyVault.Secret=$(.\key.ps1)` to launch the kubernetes app.
8)  Run `helm upgrade language . --set Configuration.KeyVault.Secret=$(.\key.ps1)` to update the app after making changes to a yaml file.

Investigation/Observation

- `kubectl get all`
- `kubectl logs <pod> --follow`
- `kubectl get services --watch`
- `kubectl proxy` (then open a web browser to `http://localhost:8001/api/v1/namespaces/kube-system/services/kubernetes-dashboard/proxy/#!/overview?namespace=default`)