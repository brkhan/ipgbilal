properties {
  $soln = $paramsoln_dir
  $build_artifacts_dir = $parambuildartifacts_dir
}


FormatTaskName "----------------- {0} ------------------"

task default -depends Deployment

task Clean{
if (Test-Path $build_artifacts_dir){
  rd  $build_artifacts_dir -recurse -force | out-null

}

mkdir $build_artifacts_dir

Write-Host "Cleaning $soln" -ForegroundColor Green
	Exec { msbuild "$soln" /t:Clean /p:Configuration=Release /v:minimal } 

 Write-Host 'Executed Clean!'
}

task Compile -depends Clean {
Write-Host "Compiling $soln" -ForegroundColor Green
Write-Host "artifacts $build_artifacts_dir" -ForegroundColor Green
Exec {
msbuild "$soln" /t:Build /p:Configuration=Release /p:OutputPath=$build_artifacts_dir  /v:minimal}
 Write-Host 'Executed Compile!'
}
 

task Test -depends Compile, Clean {
 Write-Host 'Executing  Tests!' -ForegroundColor Green
   exec {
	& ..\NUnit-3.4.1\bin\nunit3-console.exe  $build_artifacts_dir\UnitsTests.dll
	 }
 
}

task Deployment -depends Test {
 Write-Host 'All tests successful  Tests1!' -ForegroundColor Green

}

task Quick {
 Write-Host 'Executing  Tests!' -ForegroundColor Green
   exec {
	& ..\NUnit-3.4.1\bin\nunit3-console.exe  $build_artifacts_dir\UnitsTests.dll 
	 }

}