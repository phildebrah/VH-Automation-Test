$path = "."
$Variable1 = $env:AzureAd:ClientId
$JsonVariables = $Variable1 | ConvertTo-Json
$JsonVariables | Out-file $path\JsonVariables.json
$JsonVariables | write-output