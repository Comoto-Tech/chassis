@echo off
IF NOT EXIST packages\FAKE\tools\Fake.exe (
    ".nuget\NuGet.exe" "Install" ".nuget\packages.config" "-OutputDirectory" "packages" "-ExcludeVersion" || goto :done
    ".nuget\NuGet.exe" "restore" "-OutputDirectory" "packages" || goto :done
)

"packages\FAKE\tools\Fake.exe" build.fsx %*
:done
exit /b %errorlevel%