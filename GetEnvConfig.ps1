[CmdletBinding()]
$path = "."
$Variables = get-variable
$JsonVariables = $Variables | ConvertTo-Json
$JsonVariables | Out-file $path\JsonVariables.json
$JsonVariables | write-output