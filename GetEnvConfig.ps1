$path = "."
$Variables = $env
$JsonVariables = $Variables | ConvertTo-Json
$JsonVariables | Out-file $path\JsonVariables.json
$JsonVariables | write-output