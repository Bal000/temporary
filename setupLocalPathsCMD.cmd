
@echo off
chcp 65001
c:

set "WebFolder=C:\WebApps"
set "IISLocalUserGroup=IIS-SvcUsers"
set "SvcWebAccount=svcMyWebApp"
set "SvcWebApiAccount=svcMyWebApi"
set "AspNetTemp=C:\Windows\Microsoft.NET\Framework64\v4.0.30319\Temporary ASP.NET Files"
goto :MakeDirs

:MakeDirs
if exist "%WebFolder%" rd %WebFolder% /S /Q
mkdir %WebFolder%
if %errorlevel% equ 0 goto :MakeLinks
echo.
echo An error occured while trying to create directory %WebFolder%
pause >nul

:MakeLinks
cd %WebFolder%
if exist %WebFolder%\MyWebApp rd %WebFolder%\MyWebApp /S /Q
mklink /D "%WebFolder%\MyWebApp" "C:\Users\Administratör\Documents\Visual Studio 2015\Projects\MyWebApp\MyWebApp"
if exist %WebFolder%\MyWebApi rd %WebFolder%\MyWebApi /S /Q
mklink /D "%WebFolder%\MyWebApi" "C:\Users\Administratör\Documents\Visual Studio 2015\Projects\MyWepApi\MyWepApi"
echo.


echo Trying to add local user group %IISLocalUserGroup%
net localgroup %IISLocalUserGroup% /add
if %errorlevel% neq 0 call :Error "Error occurred while trying to add local user group %IISLocalUserGroup%"
echo Added local user group %IISLocalUserGroup%
echo.

echo Trying to add service account %SvcWebApiAccount% to local user group %IISLocalUserGroup%
net localgroup "%IISLocalUserGroup%" "%SvcWebApiAccount%" /add
if %errorlevel% neq 0 call :Error "Error occured while trying to add service account %SvcWebApiAccount% to local user group %IISLocalUserGroup%"
echo Added %SvcWebApiAccount% to local user group %IISLocalUserGroup%
echo.

echo Adding %IISLocalUserGroup% to %AspNetTemp%
icacls "%AspNetTemp%" /grant "%IISLocalUserGroup%":(OI)(CI)F /T
if %errorlevel% neq 0 call :Error "Error while trying to add %IISLocalUserGroup% to %AspNetTemp%"
echo Added %IISLocalUserGroup% to %AspNetTemp%
echo.

echo.
pause
exit /b

:Error
echo.
echo %1
pause >nul