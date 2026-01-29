@echo off
set _pluginName=Hi3Helper.Plugin.Endfield
set _isRemoveSymbol=true

set currentPath=%~dp0
set projectPath=%currentPath%%_pluginName%
set indexerPath=%currentPath%Indexer
set projectPublishPath=%projectPath%\publish
set indexerPublishPath=%indexerPath%\Compiled
set indexerToolPath=%currentPath%Indexer.exe
set thread=%NUMBER_OF_PROCESSORS%
set args1=%1

%~d0
cd %currentPath%

:CompileSpeedPreference
set speedChoice=%args1%
if "%speedChoice%" == "" (
    echo Please define which optimization to use:
    echo   1. Size ^(same as -O1 optimization with debug info and stack trace stripped^)
    echo   2. Speed ^(same as -O2 optimization with debug info and stack trace stripped^)
    echo   3. Debug ^(no optimization^)
    echo   4. Size-ReflectionFree ^(same as 1. Size but more lightweight with some features removed^)
    echo   5. Speed-ReflectionFree ^(same as 2. Speed but more lightweight with some features removed^)
    echo   6. Debug-ReflectionFree ^(same as 3. Debug but more lightweight with some features removed^)
    echo.
    echo Note:
    echo ReflectionFree builds is EXPERIMENTAL and might results to unexpected behaviour!
    echo Some reflection-dependant feature ^(for example: Game Installation auto-detection^) might not work.
    echo.
    set /p speedChoice=Choice^?^> 
)

if "%speedChoice%" == "1" (
  echo Compiling with Size preferred optimization
  set publishProfile=ReleasePublish-O1
  set configuration=Release
  goto :StartCompilation
) else if "%speedChoice%" == "2" (
  echo Compiling with Speed preferred optimization
  set publishProfile=ReleasePublish-O2
  set configuration=Release
  goto :StartCompilation
) else if "%speedChoice%" == "3" (
  echo Compiling with No Optimization ^(Debug^)
  set publishProfile=DebugPublish
  set configuration=Debug
  set _isRemoveSymbol=false
  goto :StartCompilation
) else if "%speedChoice%" == "4" (
  echo Compiling with Size preferred optimization + Reflection-Free Mode
  set publishProfile=ReleaseNoReflectionPublish-O1
  set configuration=ReleaseNoReflection
  goto :StartCompilation
) else if "%speedChoice%" == "5" (
  echo Compiling with Speed preferred optimization + Reflection-Free Mode
  set publishProfile=ReleaseNoReflectionPublish-O2
  set configuration=ReleaseNoReflection
  goto :StartCompilation
) else if "%speedChoice%" == "6" (
  echo Compiling with No Optimization ^(Debug^) + Reflection-Free Mode
  set publishProfile=DebugNoReflectionPublish
  set configuration=DebugNoReflection
  set _isRemoveSymbol=false
  goto :StartCompilation
)

cls
echo Input is not valid! Available choices: 1, 2, 3, 4, 5 or 6
set publishProfile=
set args1=
goto :CompileSpeedPreference

:StartCompilation
set outputBaseDirPath=%projectPublishPath%\%configuration%
set outputDirPath=%outputBaseDirPath%
%~d0

:StartPluginCompilation
if /I exist "%projectPublishPath%" (
    rmdir /S /Q "%projectPublishPath%" || goto :Error
)
mkdir "%outputDirPath%"
cd "%projectPath%"
dotnet restore --runtime win-x64 ..\Hi3Helper.Plugin.Endfield.sln || goto :Error
dotnet clean --configuration %configuration% --runtime win-x64 ..\Hi3Helper.Plugin.Endfield.sln || goto :Error
dotnet publish --configuration %configuration% --runtime win-x64 /p:PublishProfile=%publishProfile% -o "%outputDirPath%" || goto :Error

:RemovePDBIfNotDebug
if "%_isRemoveSymbol%" == "true" (
  del "%outputDirPath%\*.pdb" || goto :Error
)

:StartIndexing
%indexerToolPath% %outputDirPath% || goto :Error

goto :CompileSuccess

:Error
echo An error has occurred while performing compilation with error status: %errorlevel%
goto :End

:CompileSuccess
echo The plugin has been compiled successfully!
echo Go to this path to see the compile output:
echo   %outputBaseDirPath%
echo.
goto :End

:End
if "%args1%" == "" (
    pause | echo Press any key to exit...
)
cd "%currentPath%"
