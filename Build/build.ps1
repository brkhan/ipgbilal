Import-module ..\packages\psake.4.6.0\tools\psake.psm1

properties {
$solnfile = "../iPgBilal.sln"
$build_artifacts_dir_loc = "../BuildArtifacts"
}

task default -depends start

task start {
Invoke-psake default.ps1  -parameters @{paramsoln_dir ="../iPgBilal.sln"; parambuildartifacts_dir = "../BuildArtifacts"} -framework 4.5.2x64
}
