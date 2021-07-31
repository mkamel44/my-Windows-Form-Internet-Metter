#define MyAppName "kamel_Metter"
#define MyAppVersion "2.0"
#define MyAppExeName "kamel_Metter.exe"
#define MyAppPublisher "„Õ„œ ﬂ«„·"

[Setup]
AppId={{CCB21BD4-DA1F-4E24-A02E-9824E32CE1C5}
AppName={#MyAppName} {#MyAppVersion}
AppVersion={#MyAppVersion}
AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
DefaultDirName={pf}\{#MyAppName}
DefaultGroupName={#MyAppName}
AllowNoIcons=yes
OutputDir=.
OutputBaseFilename=kamel_Metter_setup
Compression=lzma
SolidCompression=yes
SetupIconFile=".\app.ico"

[Languages]
Name: "arabic"; MessagesFile: "compiler:Languages\Arabic.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked
Name: "quicklaunchicon"; Description: "{cm:CreateQuickLaunchIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked; OnlyBelowVersion: 0,6.1

[Files]
Source: ".\kamel_Metter.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: ".\alert.wav"; DestDir: "{app}"; Flags: ignoreversion
Source: ".\Microsoft.Win32.TaskScheduler.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: ".\DropDownPanel.dll"; DestDir: "{app}"; Flags: ignoreversion

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppName}"
Name: "{group}\{cm:UninstallProgram,{#MyAppName}}"; Filename: "{uninstallexe}"
Name: "{commondesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppName}"; Tasks: desktopicon
Name: "{userappdata}\Microsoft\Internet Explorer\Quick Launch\{#MyAppName}"; Filename: "{app}\{#MyAppName}"; Tasks: quicklaunchicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Flags: nowait postinstall runascurrentuser skipifsilent; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"

[Code]

 Function InitializeSetup : Boolean;
var winHwnd: longint;
    retVal : boolean;
    ResultCode: Integer;
begin
  Result := true;
  try
    winHwnd := FindWindowByWindowName('{#MyAppName}');
    Log('winHwnd: ' + inttostr(winHwnd));
    if winHwnd <> 0 then
      retVal:=postmessage(winHwnd,18,0,0);
        Result := True
  except
  end;
end;


{
function NextButtonClick(CurPageID: Integer): Boolean;
var
  ResultCode: Integer;
begin
  case CurPageID of
    wpSelectDir:
     begin
      Exec('SCHTASKS.exe', '/Delete /TN "{#MyAppName}" /F', '', SW_HIDE,ewWaitUntilTerminated, ResultCode);
      Exec('SCHTASKS.exe', '/Create  /TN "{#MyAppName}" /SC ONLOGON /TR "\"' + WizardDirValue + '\{#MyAppExeName}\"" /RL HIGHEST', '', SW_HIDE,ewWaitUntilTerminated, ResultCode);
     end;
   end;

   Result := True
  end;     
}

  
Function InitializeUninstall : Boolean;
var winHwnd: longint;
    retVal : boolean;
    ResultCode: Integer;
begin
  Exec('SCHTASKS.exe', '/Delete /TN "{#MyAppName}" /F', '', SW_HIDE,ewWaitUntilTerminated, ResultCode);
  Result := true;
  try
    winHwnd := FindWindowByWindowName('{#MyAppName}');
    Log('winHwnd: ' + inttostr(winHwnd));
    if winHwnd <> 0 then
      retVal:=postmessage(winHwnd,18,0,0);
        Result := True
  except
  end;
end;