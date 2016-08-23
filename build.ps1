#cls

#Set-ExecutionPolicy -ExecutionPolicy RemoteSigned

remove-module [p]sake

#Import-Module "..\packages\psake.4.6.0\tools\psake.psm1"

Write-Host $Env:PWD

# find psake's path
$psakeModule = (Get-ChildItem (".\Packages\psake*\tools\psake.psm1")).FullName | Sort-Object $_ | select -last 1
 
Import-Module $psakeModule

properties {
$solnfile = "..\iPgBilal.sln"
$build_artifacts_dir_loc = "..\BuildArtifacts"
$packageLoc = "..\Build\PublishedWebsites"
$webProject = "..\iPagooBilal\iPgBilal.csproj"
}

#task default -depends start

#task start {
#Invoke-psake Build/default.ps1  -parameters @{paramsoln_dir ="$solnfile"; parambuildartifacts_dir ="$build_artifacts_dir_loc"; paramPublishedWeb = "$webProject"; paramWebPackageLoc="$packageLoc"} -framework 4.5.2x64
#}

#task start {
	Write-Host 'usermame ' $paramusername
	Write-Host 'password ' $parampassword

	Write-Host 'usermame ' $args[0]
	Write-Host 'password ' $args[1]
Invoke-psake Build/default.ps1  -parameters @{paramsoln_dir ="..\iPgBilal.sln"; parambuildartifacts_dir = "..\BuildArtifacts"; paramPublishedWeb = "..\iPagooBilal\iPgBilal.csproj"; paramWebPackageLoc="..\Build\PublishedWebsites"; paramWebDeployExe="C:\Program Files\IIS\Microsoft Web Deploy V3\\"; paramusername=$paramusername; parampassword=$parampassword} -framework 4.5.2x64
#}

Write-Host "Build exit code:" $LastExitCode

# Propagating the exit code so that builds actually fail when there is a problem
exit $LastExitCode

