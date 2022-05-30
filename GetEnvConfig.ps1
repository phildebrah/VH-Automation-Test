cd Env:
Get-ChildItem  | write-output
[System.Environment]::GetEnvironmentVariables() | write-output
$path = "."

$Variable1 = $env:ClientId
$JsonVariables = $Variable1 | ConvertTo-Json
$JsonVariables | Out-file $path\JsonVariables.json
$JsonVariables | write-output