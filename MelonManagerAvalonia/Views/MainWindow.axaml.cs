using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace MelonManagerAvalonia.Views
{
    public partial class MainWindow : Window
    {
        public static Window Instance { get; set; }
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void AboutPageView_OnInitialized(object? sender, EventArgs e)
        {
            var aboutControl = sender as UserControl;
            aboutControl.Content = new AboutWindow();
        }

        private void StyledElement_OnInitialized(object? sender, EventArgs e)
        {
            Instance = sender as MainWindow;
        }
    }
}