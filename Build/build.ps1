cls

Set-ExecutionPolicy -ExecutionPolicy RemoteSigned

remove-module [p]sake

#Import-Module "..\packages\psake.4.6.0\tools\psake.psm1"

# find psake's path
$psakeModule = (Get-ChildItem ("..\Packages\psake*\tools\psake.psm1")).FullName | Sort-Object $_ | select -last 1
 
Import-Module $psakeModule

properties {
$solnfile = "../iPgBilal.sln"
$build_artifacts_dir_loc = "../BuildArtifacts"
}

#task default -depends start

#task start {
Invoke-psake default.ps1  -parameters @{paramsoln_dir ="../iPgBilal.sln"; parambuildartifacts_dir = "../BuildArtifacts"} -framework 4.5.2x64
#}

Write-Host "Build exit code:" $LastExitCode

# Propagating the exit code so that builds actually fail when there is a problem
exit $LastExitCode
