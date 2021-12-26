using System;
using System.IO;
using Tomlet;

namespace MelonManagerAvalonia.Managers;

public class Config<T> where T : new()
{
    public readonly string name;
    public readonly string filePath;
    public T config;
        
    public Config(string configName)
    {
        name = configName;
        filePath = Path.Combine(Program.localFilesPath, configName + ".cfg");
        Load();
    }

    public void Load()
    {
        Logger.Log($"Loading config '{name}'");
        if (!File.Exists(filePath))
        {
            config = new T();
            return;
        }

        string filestr = File.ReadAllText(filePath);
        if (string.IsNullOrEmpty(filestr))
            return;
        try 
        { 
            config = TomletMain.To<T>(filestr); 
        } 
        catch (Exception ex)
        {
            Logger.Log($"Failed to load the config '{name}':\n{ex}", Logger.Level.Error);
        }
    }

    public void Save()
    {
        try
        {
            File.WriteAllText(filePath, TomletMain.TomlStringFrom(config));
        }
        catch (Exception ex)
        {
            Logger.Log($"Failed to save config '{name}':\n{ex}", Logger.Level.Error);
            //TODO CustomMessageBox.Error($"Failed to save config '{name}':\n\n{ex}");
            return;
        }

        Logger.Log($"Saved config '{name}'!");
    }
}