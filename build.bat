@ECHO OFF

SET MSBuild=%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe

SET BuildConfigurations=Debug,Release,Release.Plus.AutoPoco,Release.Plus.NBuilder
SET BuildTargets=Clean,Build
SET VerbosityLevel=/v:minimal
SET UseAllProcessors=/m

FOR %%G IN (%BuildConfigurations%) DO (
	%MSBuild% NTestData.sln /t:%BuildTargets%  /p:Configuration=%%G %VerbosityLevel% %UseAllProcessors% /nologo
)

PAUSE
