@Echo Off
SetLocal EnableDelayedExpansion

PushD "%~dp0"

Set "JenkinsTrayExe=..\JenkinsTray\bin\x86\Release\JenkinsTray.exe"
Set "FileVerExe=..\Lib\Tools\filever.exe"

for /F "usebackq tokens=4* delims= " %%F in (`%FileVerExe% /B /A /D %JenkinsTrayExe%`) do (
    Set Version=%%F
)

echo The file version is %Version%