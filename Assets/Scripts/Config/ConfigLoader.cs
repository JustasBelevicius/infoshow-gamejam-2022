using System.IO;
using UnityEngine;

public static class ConfigLoader
{
    private const string CONFIG_FILE = "config.json";
    private static Config _config;

    public static Config getConfig() {
        if (_config == null) {
            _config = loadConfig();
        }
        return _config;
    }

    private static Config loadConfig() {
        var configPath = Path.Combine(Application.streamingAssetsPath, CONFIG_FILE);
        var json = System.IO.File.ReadAllText(configPath);
        return JsonUtility.FromJson<Config>(json);
    }
}
