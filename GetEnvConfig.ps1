write-output "Env"
write-output ""
cd Env:
Get-ChildItem  | write-output
write-output ""
write-output "[System.Environment]"
[System.Environment]::GetEnvironmentVariables() | write-output
write-output ""
$path = "."
#$Variable1 = $env:ClientId
$Variable1 = Get-ChildItem
$JsonVariables = $Variable1 | ConvertTo-Json -Depth 10
# Depth of 3 no good check 10
$JsonVariables | Out-file $path\JsonVariables.json
$JsonVariables | write-output