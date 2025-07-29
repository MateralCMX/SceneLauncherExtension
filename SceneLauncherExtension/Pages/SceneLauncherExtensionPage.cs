using Materal.Extensions;
using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;
using SceneLauncherExtension.Pages;
using System.Collections.Generic;
using System.IO;

namespace SceneLauncherExtension;

internal sealed partial class SceneLauncherExtensionPage : ListPage
{
    private SceneLauncherConfig? _config;
    private readonly OpenSettingListItem _settingItem;
    public SceneLauncherExtensionPage()
    {
        Icon = IconHelpers.FromRelativePath("Assets\\StoreLogo.png");
        Title = "场景启动";
        Name = "Open";
        _settingItem = new();
        _settingItem.OnOpenSettings += SettingItem_OnOpenSettings;
    }
    private void SettingItem_OnOpenSettings(object? sender, System.EventArgs e) => _config = null;
    public override IListItem[] GetItems()
    {
        List<IListItem> items = [];
        if (_config is null)
        {
            string tempPath = Path.GetTempPath();
            string settingDirectoryPath = Path.Combine(tempPath, "SceneLauncher");
            if (!Directory.Exists(settingDirectoryPath))
            {
                Directory.CreateDirectory(settingDirectoryPath);
            }
            string settingPath = Path.Combine(settingDirectoryPath, "setting.json");
            if (File.Exists(settingPath))
            {
                string json = File.ReadAllText(settingPath);
                _config = json.JsonToObject<SceneLauncherConfig>();
            }
        }
        if (_config is not null)
        {
            foreach (SceneConfig item in _config.Items)
            {
                items.Add(new SceneListItem(item));
            }
        }
        items.Add(_settingItem);
        return [.. items];
    }
}
