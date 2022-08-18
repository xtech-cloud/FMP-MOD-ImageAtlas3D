
@echo off

REM !!! Generated by the fmp-cli 1.19.0.  DO NOT EDIT!

set WORK_DIR=%cd%
set /P UNITY_HOME=<.UNITY_HOME.env

mkdir _build_
mkdir _dist_
copy Unity.csproj.keep _build_\Unity.csproj
xcopy /Q/S/Y ImageAtlas3D\Assets\Scripts\Module _build_\Module\
cd _build_
powershell -Command "(gc Unity.csproj) -replace '{{UNITY_HOME}}', '%UNITY_HOME%' | Out-File Unity.csproj"
powershell -Command "(gc Unity.csproj) -replace '{{WORK_DIR}}', '%WORK_DIR%' | Out-File Unity.csproj"
dotnet build -c=Release
cd ..
DEL /Q/S _build_\bin\Release\netstandard2.1\Unity*
move _build_\bin\Release\netstandard2.1\*.Unity.dll .\_dist_\
RD /Q/S _build_
pause
