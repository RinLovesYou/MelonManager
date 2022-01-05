using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MelonManagerAvalonia.Games;
using MelonManagerAvalonia.Installer;

namespace MelonManager.Views;

public class LibraryWindow : UserControl
{
    public LibraryWindow()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        var path = OpenUnityGameDialog.Open();
        if (path == null)
            return;
        if (LibraryGame.GetLibGame(path) != null)
        {
            //TODO CustomMessageBox.Error("This game already exists in your library!");
            return;
        }
        var info = GameInfo.Create(path);
        if (info == null)
            return;
        var g = LibraryGame.AddGame(info, false);
        if (g.ml == null && new InstallerForm(g).ShowDialog() != DialogResult.OK)
        {
            g.Remove();
            return;
        }
    }
}