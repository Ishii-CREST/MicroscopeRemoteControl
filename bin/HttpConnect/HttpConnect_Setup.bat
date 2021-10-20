::Setup bat file for HttpConnect

:: Please placement all files to [SetupFiles]Directory,
:: that would copied in Installer
::セットアップ行う場合は本バッチファイルと同階層のSetupFiles下に
::コピー対象のファイルをすべておいて使用すること

@echo off

set NISPATH=C:\Program Files\NIS-Elements\
set APPPATH=%NISPATH%HttpConnect\
set MACPATH=%NISPATH%Macros\
set ORG_FILE_PATH=%APPPATH%OrgFile\

echo Input number.
echo  Install HttpConnect   ⇒ 1
echo  Uninstall HttpConnect ⇒ 0
echo                 Exit   ⇒ 9

set /p NUM=" >"

:LOOP

if %NUM% == 0 goto delete
if %NUM% == 1 goto setup
if %NUM% == 9 goto end

if %NUM% NEQ 0 if %NUM% NEQ 1 goto error

:setup

md "%NISPATH%"
md "%APPPATH%"
md "%ORG_FILE_PATH%"

copy "SetupFiles\HttpConnect.exe" "%APPPATH%"
copy "SetupFiles\HttpConnect.exe.config" "%APPPATH%"
copy "SetupFiles\HttpConnect.Core.dll" "%APPPATH%"
copy "SetupFiles\NisMacro.Net.Execute.dll" "%NISPATH%"
copy "SetupFiles\NisMacro.Net.Execute.dll" "%APPPATH%"
copy "SetupFiles\NisMacro.Net.Interprocess.dll" "%NISPATH%"
copy "SetupFiles\NisMacro.Net.Macro.dll" "%NISPATH%"
copy "SetupFiles\NisMacro.Net.Setting.dll" "%NISPATH%"
copy "SetupFiles\NisMacro.Net.Util.dll" "%NISPATH%"
copy "SetupFiles\NisMacro.Net.Util.dll" "%APPPATH%"

copy "SetupFiles\macros\HttpConnect.mac" "%MACPATH%"
copy "SetupFiles\macros\NisMacro.Net_Inc.mac" "%MACPATH%"

copy "SetupFiles\OrgFile\web.config" "%ORG_FILE_PATH%"

goto end


:delete

rd /s /q "%APPPATH%"

del "%MACPATH%HttpConnect.mac"
del "%MACPATH%NisMacro.Net_Inc.mac"

goto end

:error
echo 0,1または9を入力してください。
goto LOOP

:end

pause
