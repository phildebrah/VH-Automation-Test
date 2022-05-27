	
# Enable -Verbose option
[CmdletBinding()]
Get-ChildItem -Path Env:\ | Format-List
Get-Variable 