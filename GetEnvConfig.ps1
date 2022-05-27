[CmdletBinding()]
$Variables = get-variable
$JsonVariables = $Variables | ConvertTo-Json
$JsonVariables | Out-file H:\Users\mkean\source\repos\VH-Automation-Test\JsonVariables.json