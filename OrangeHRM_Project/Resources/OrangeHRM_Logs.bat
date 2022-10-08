@ECHO OFF
ECHO Demo Automation Executed Started.


set dllpath=D:\ContourSoftwareAutomation\Project\OrangeHRM_Project\OrangeHRM_Project\bin\Debug\OrangeHRM_Project.dll
set trxerpath=D:\ContourSoftwareAutomation\Project\OrangeHRM_Project\OrangeHRM_Project\
set SummaryReportPath=D:\ContourSoftwareAutomation\Project\OrangeHRM_Project\OrangeHRM_Project

FOR /f %%a IN ('WMIC OS GET LocalDateTime ^| FIND "."') DO SET DTS=%%a
SET filename=%testcategory%_%DTS:~0,4%%DTS:~4,2%%DTS:~6,2%%DTS:~8,2%%DTS:~10,2%%DTS:~12,2%
echo %filename%

call "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat"


VSTest.Console.exe  %dllpath%  /Logger:"trx;LogFileName=%SummaryReportPath%\%filename%.trx"

cd %trxerpath%

TrxToHTML.exe %SummaryReportPath%\

PAUSE