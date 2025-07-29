using Microsoft.CommandPalette.Extensions.Toolkit;
using SceneLauncherExtension.Pages;
using System.Diagnostics;
using System.IO;

namespace SceneLauncherExtension;

internal sealed partial class SceneListItem : ListItem
{
    private readonly SceneConfig _config;
    public SceneListItem(SceneConfig config) : base(new NoOpCommand())
    {
        _config = config;
        Command = new AnonymousCommand(action: StartProgram) { Result = CommandResult.KeepOpen() };
        Title = config.Name;
        Subtitle = config.Description;
    }

    private void StartProgram()
    {
        foreach (string programPath in _config.ProgramPaths)
        {
            try
            {
                string fileName = Path.GetFileNameWithoutExtension(programPath);
                if (!string.IsNullOrWhiteSpace(fileName))
                {
                    Process[] currectProcess = Process.GetProcessesByName(fileName);
                    if (currectProcess.Length > 0) continue;
                }
                Process.Start(new ProcessStartInfo(programPath) { UseShellExecute = true });
            }
            catch
            {
                Process.Start(new ProcessStartInfo(programPath) { UseShellExecute = true });
            }
        }
    }
}