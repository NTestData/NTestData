@echo off

set MSBuild=%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe

%MSBuild% NTestData.sln /t:Rebuild /v:m /nologo /p:Configuration=Release.Plus.NBuilder

pause
