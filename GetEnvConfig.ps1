$path = "."
$Variable1 = $env:AzureAd:ClientId
$JsonVariables = $Variables | ConvertTo-Json
$JsonVariables | Out-file $path\JsonVariables.json
$JsonVariables | write-output