; Settings
#define MyAppName "Screencast Capture Lite"
#define MyAppVersion "1.6"
#define MyAppPublisher "Accord.NET Framework"
#define MyAppURL "http://screencast-capture.googlecode.com"
#define MyAppExeName "ScreenCapture.exe"

[Setup]
AppId={{A4DAE6AA-2ACE-4905-9124-635C0F700054}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={pf}\{#MyAppName}
DefaultGroupName={#MyAppName}
AllowNoIcons=yes
LicenseFile=..\..\License.txt
OutputBaseFilename=ScreencastCaptureLite-{#MyAppVersion}
Compression=lzma
SolidCompression=yes
OutputDir=..\bin

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Components]
Name: "bin"; Description: "Program executable"; Types: full compact custom; Flags: fixed
Name: "src"; Description: "Source code files"; Types: full custom;

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "..\..\Binaries\*";        DestDir: "{app}\Binaries";  Components: bin; Flags: recursesubdirs;
Source: "..\..\Sources\*";         DestDir: "{app}\Sources";   Components: src; Flags: recursesubdirs; Excludes: "*.~*,\TestResults,\bin,\obj,*.sdf,*.suo,*.user,*.vsp,*.shfbproj_*,*.pidb"
Source: "..\..\Copyright.txt";     DestDir: "{app}";           Components: bin
Source: "..\..\License.txt";       DestDir: "{app}";           Components: bin
Source: "..\..\Release notes.txt"; DestDir: "{app}";           Components: bin

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\Binaries\{#MyAppExeName}"
Name: "{group}\{cm:UninstallProgram,{#MyAppName}}"; Filename: "{uninstallexe}"
Name: "{commondesktop}\{#MyAppName}"; Filename: "{app}\Binaries\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\Binaries\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

