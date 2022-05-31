#write-output "Env"
#write-output ""
$path = Get-Location
cd Env:
#Get-ChildItem  | write-output
write-output ""
write-output "[System.Environment]"
$JsonVariables = [System.Environment]::GetEnvironmentVariables() | ConvertTo-json
$JsonVariables | write-output
#write-output ""
#$Variable1 = $env:ClientId
#$Variable1 = Get-ChildItem
#$JsonVariables = $Variable1 | ConvertTo-Json
# Depth of 3 no good check 10
$JsonVariables | Out-file $path\JsonVariables.json
#$JsonVariables | write-output