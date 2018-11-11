$id = az keyvault secret show -n regcred-id --vault-name brwals-keys --query value
$token = az keyvault secret show -n regcred-token --vault-name brwals-keys --query value

kubectl create secret docker-registry containerpreviewregistry --docker-server=containerpreview.azurecr.io --docker-email=brwals@microsoft.com --docker-username=$id --docker-password=$token

kubectl create secret docker-registry brwalsregistry --docker-server=brwalsincubationregistry.azurecr.io --docker-email=brwals@microsoft.com --docker-username=$id --docker-password=$token

kubectl create secret generic keyvault --from-literal=appid=$id --from-literal=token=$token

# helm install -n language . --set Configuration.KeyVault.Secret=$(.\key.ps1)
# helm upgrade language . --set Configuration.KeyVault.Secret=$(.\key.ps1)
