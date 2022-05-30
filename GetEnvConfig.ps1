Import-Module Microsoft.PowerShell.Utility
$path = "."
$Variables = get-variable
$JsonVariables = $Variables | ConvertTo-Json
$JsonVariables | Out-file $path\JsonVariables.json
$JsonVariables | write-output