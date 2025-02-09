﻿using System.Reactive;
using System.Reflection;
using Avalonia.Controls;
using MelonManager.Views;
using MelonManagerAvalonia.Utils;
using MelonManagerAvalonia.Views;
using ReactiveUI;

namespace MelonManagerAvalonia.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public string VersionNumber => "v" + Constants.Version;
        
        #region Library
        
        public UserControl LibraryPage => new LibraryWindow();
        
        #endregion

        #region PackageStore

        public string PackageStoreText =>
            "This page will soon turn into a Package Store where you will be able to explore and find your\nfavorite mods, mod packs and plugins from verified external sources.";

        #endregion
        
        #region AboutPage

        public string AboutText => "MelonManager is the Official Manager App for MelonLoader.\n\n Questions? Join our Discord Server (linked below).";
        public UserControl AboutPage => new AboutWindow();
        
        public ReactiveCommand <Unit, Unit> OpenMlDiscord { get; }
        public ReactiveCommand <Unit, Unit> OpenMlWiki { get; }
        public ReactiveCommand <Unit, Unit> OpenMlGithub { get; }
        public ReactiveCommand <Unit, Unit> OpenLavaGangTwitter { get; }
        public ReactiveCommand <Unit, Unit> OpenLavaGangGithub { get; }
        public ReactiveCommand <Unit, Unit> OpenMelonManagerGithub { get; }
        public ReactiveCommand <Unit, Unit> OpenSlidyDev { get; }
        public ReactiveCommand <Unit, Unit> OpenSamboy { get; }
        public ReactiveCommand <Unit, Unit> OpenRin { get; }

        public MainWindowViewModel()
        {
            OpenMlDiscord = ReactiveCommand.Create(() => GenericUtils.OpenBrowser(Constants.Discord));
            OpenMlWiki = ReactiveCommand.Create(() => GenericUtils.OpenBrowser(Constants.Wiki));
            OpenMlGithub = ReactiveCommand.Create(() => GenericUtils.OpenBrowser(Constants.MLGitHub));
            OpenLavaGangTwitter = ReactiveCommand.Create(() => GenericUtils.OpenBrowser(Constants.Twitter));
            OpenLavaGangGithub = ReactiveCommand.Create(() => GenericUtils.OpenBrowser(Constants.LavaGang));
            OpenMelonManagerGithub = ReactiveCommand.Create(() => GenericUtils.OpenBrowser(Constants.MMGitHub));
            OpenSlidyDev = ReactiveCommand.Create(() => GenericUtils.OpenBrowser(Constants.SlidyDevGit));
            OpenSamboy = ReactiveCommand.Create(() => GenericUtils.OpenBrowser(Constants.SamboyGit));
            OpenRin = ReactiveCommand.Create(() => GenericUtils.OpenBrowser(Constants.RinGit));
        }
        
        #endregion
    }
}