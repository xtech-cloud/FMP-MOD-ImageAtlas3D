
@echo off

REM !!! Generated by the fmp-cli 1.59.0.  DO NOT EDIT!

md ImageAtlas3D\Assets\3rd\fmp-xtc-imageatlas3d

cd ..\vs2022
dotnet build -c Release

copy fmp-xtc-imageatlas3d-lib-mvcs\bin\Release\netstandard2.1\*.dll ..\unity2021\ImageAtlas3D\Assets\3rd\fmp-xtc-imageatlas3d\
