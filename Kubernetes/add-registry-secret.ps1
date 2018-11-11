param (
    [Parameter(Mandatory=$true)][string]$KeyVault,
    [Parameter(Mandatory=$true)][string]$Registry,
    [Parameter(Mandatory=$true)][string]$Email,
    [Parameter(Mandatory=$true)][string]$SecretName,
    [Parameter(Mandatory=$true)][string]$Namespace
)

$id = az keyvault secret show -n regcred-id --vault-name $KeyVault --query value
$token = az keyvault secret show -n regcred-token --vault-name $KeyVault --query value

kubectl create secret docker-registry $SecretName --docker-server=$Registry --docker-email=$Email --docker-username=$id --docker-password=$token --namespace $Namespace
