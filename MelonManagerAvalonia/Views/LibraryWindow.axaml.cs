using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

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
}