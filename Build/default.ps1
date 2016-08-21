properties {
  $soln = $paramsoln_dir
  $build_artifacts_dir = $parambuildartifacts_dir
  $proj = $paramPublishedWeb
  $webPackageLoc_dir = $paramWebPackageLoc
}


FormatTaskName "----------------- {0} ------------------"

task default -depends Deployment

task Clean{
if (Test-Path $build_artifacts_dir){
  rd  $build_artifacts_dir -recurse -force | out-null

}

if (Test-Path  $webPackageLoc_dir){
  rd   $webPackageLoc_dir -recurse -force | out-null

}

mkdir $build_artifacts_dir
mkdir  $webPackageLoc_dir 

Write-Host "Cleaning $soln" -ForegroundColor Green
	Exec { msbuild "$soln" /t:Clean /p:Configuration=Release /v:minimal } 
	
 Write-Host 'Executed Clean!'
}

task Compile -depends Clean {
Write-Host "Compiling  resolve-path $soln" -ForegroundColor Green
Write-Host "artifacts resolve-path $build_artifacts_dir" -ForegroundColor Green
Write-Host "web project resolve-path $proj" -ForegroundColor Green
Write-Host "web package location  resolve-path $webPackageLoc_dir" -ForegroundColor Green

Exec { msbuild "$soln" /t:Build /p:Configuration=Release /p:OutputPath=$build_artifacts_dir  /v:minimal}
 Write-Host 'Executed Compile!'

		if($LASTEXITCODE -ne 0) {
        throw "Failed to deploy to Release"
        exit 1
    }

}
 

task Test -depends Compile, Clean {
 Write-Host 'Executing  Tests!' -ForegroundColor Green
   exec {
	& ..\NUnit-3.4.1\bin\nunit3-console.exe  $build_artifacts_dir\UnitsTests.dll
	 }

	 Write-Host 'All tests successful  Tests1!' -ForegroundColor Green
 
}

task Deployment -depends Test  {
	
 Exec { msbuild "$proj" /t:Package /p:Platform=AnyCPU /p:Configuration=Release /p:PackageLocation="$webPackageLoc_dir" /v:q }

Write-Host 'Executed Package creation!' -ForegroundColor Green


	if($LASTEXITCODE -ne 0) {
        throw "Failed to deploy to Release"
        exit 1
    }


}

task Quick {
 Write-Host 'Executing  Tests!' -ForegroundColor Green
   exec {
	& ..\NUnit-3.4.1\bin\nunit3-console.exe  $build_artifacts_dir\UnitsTests.dll 
	 }

}