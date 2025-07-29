using Materal.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;
using SceneLauncherExtension.Pages;
using System;
using System.Diagnostics;
using System.IO;

namespace SceneLauncherExtension;

internal sealed partial class OpenSettingListItem : ListItem
{
    public event EventHandler? OnOpenSettings;
    public OpenSettingListItem() : base(new NoOpCommand())
    {
        Command = new AnonymousCommand(action: OpenSettingFile) { Result = CommandResult.KeepOpen() };
        Title = "Settings";
        Subtitle = "设置";
    }
    private void OpenSettingFile()
    {
        string tempPath = Path.GetTempPath();
        string settingDirectoryPath = Path.Combine(tempPath, "SceneLauncher");
        if (!Directory.Exists(settingDirectoryPath))
        {
            Directory.CreateDirectory(settingDirectoryPath);
        }
        string settingPath = Path.Combine(settingDirectoryPath, "setting.json");
        if (!File.Exists(settingPath))
        {
            SceneLauncherConfig config = new();
            config.Items.Add(new SceneConfig()
            {
                Name = "Example Scene",
                Description = "示例场景",
                ProgramPaths = [
                    "C:\\Program Files\\WindowsApps\\Microsoft.WindowsNotepad_11.2504.62.0_x64__8wekyb3d8bbwe\\Notepad\\Notepad.exe"
                ]
            });
            string configContent = config.ToJson();
            File.WriteAllText(settingPath, configContent);
        }
        OnOpenSettings?.Invoke(this, EventArgs.Empty);
        Process.Start(new ProcessStartInfo(settingPath) { UseShellExecute = true });
    }
}
