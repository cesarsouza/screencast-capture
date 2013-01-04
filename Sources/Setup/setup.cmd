@echo off

echo.
echo Screencast Capture Lite compressed archive builder
echo =========================================================
echo. 
echo This Windows batch file uses WinRAR to automatically
echo build the compressed archive version of the application.
echo. 


:: Settings for complete package creation
:: ---------------------------------------------------------

set version=1.1
set fullname="ScreencastCaptureLite-%version%.zip" 
set output=..\bin\
set rar="C:\Program Files\WinRAR\winrar"
set opts=a -m5 -s -afzip

set output=%output%%fullname%

echo  - Version : %version%
echo  - Filepath: %fullname%     
echo  - Output  : %output%

echo.
echo  - WinRAR Command: %rar%
echo  - WinRAR Options: "%opts%"
echo.

pause

echo.
echo.
echo Creating %fullname% archive
echo ---------------------------------------------------------

del %output%

%rar% %opts%    %output% "..\..\Copyright.txt"
%rar% %opts%    %output% "..\..\License.txt"
%rar% %opts%    %output% "..\..\Instructions.txt"
%rar% %opts%    %output% "..\..\Release notes.txt"
%rar% %opts% -r %output% "..\..\Binaries\*"        -x*\.svn* -x*.lastcodeanalysissucceeded -x*.CodeAnalysisLog.xml -x*SlimDX.pdb
%rar% %opts% -r %output% "..\..\Sources\*"         -x*\.svn* -x*\obj -x*\bin -x*\TestResults -x*.sdf -x*.suo -x*.user -x*.shfbproj_* -x*.vsp -x*.pidb
%rar% t         %output%


echo.
echo ---------------------------------------------------------
echo Package creation has completed. Please check the above
echo commands for errors and check packages in output folder.
echo ---------------------------------------------------------
echo.

pause
