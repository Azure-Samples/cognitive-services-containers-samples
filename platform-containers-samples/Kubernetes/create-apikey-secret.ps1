param (
    [Parameter(Mandatory=$true)][string]$SecretName,
    [Parameter(Mandatory=$true)][string]$APIKey,
    [Parameter(Mandatory=$true)][string]$Namespace
)

kubectl create secret generic $SecretName --from-literal=apikey=$APIKey --namespace $Namespace