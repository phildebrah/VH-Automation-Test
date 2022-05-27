[CmdletBinding()]
$path = "H:\Users\mkean\source\repos\VH-Automation-Test"
$Variables = get-variable
$JsonVariables = $Variables | ConvertTo-Json
$JsonVariables | Out-file $path\JsonVariables.json