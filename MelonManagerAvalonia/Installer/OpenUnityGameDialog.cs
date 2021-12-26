using System;
using System.IO;
using Avalonia.Controls;
using MelonManagerAvalonia.ViewModels;
using MelonManagerAvalonia.Views;

namespace MelonManagerAvalonia.Installer;

public static class OpenUnityGameDialog
{
    public static string Open()
    {
        return OpenDia(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
    }

    private static string OpenDia(string initPath)
    {
        var dia = new OpenFileDialog();
        dia.Title = "Open Unity Game";
        dia.Filters.Add(new FileDialogFilter() { Name = "Unity Game", Extensions = {"exe", "lnk", "url" } });
        dia.Directory = initPath;
        var result = dia.ShowAsync(MainWindow.Instance);
        while (!result.IsCompleted) { }
        if (result.Result == null)
            return null;
        var path = PathFinder.FindPathOfExe(result.Result[0]);
        var ext = Path.GetExtension(path);
        if (ext == ".exe")
            return path;
        if (string.IsNullOrEmpty(ext) && Directory.Exists(path))
            return OpenDia(path);
        return null;
    }
}