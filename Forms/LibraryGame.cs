using System;
using System.Drawing;
using System.Linq;
using MetroFramework.Controls;
using System.IO;
using System.Diagnostics;

namespace MelonLauncher.Forms
{
    public partial class LibraryGame : MetroUserControl
    {
        public Info info;
        public MLInfo ml;

        private LibraryGame(Info info, MLInfo ml)
        {
            this.info = info;
            this.ml = ml;
            InitializeComponent();
            gameName.Text = info.name;
            gameAuthor.Text = info.author;
            gamePicture.Image = Icon.ExtractAssociatedIcon(info.path).ToBitmap();

            VerifyVersion();
        }

        public void VerifyVersion()
        {
            MLVersion.Text = "ML v" + ml.version;

            if (Program.LatestVersion == null)
            {
                updateButton.Enabled = false;
                updateButton.Text = "Updater Offline";
                return;
            }
            bool isLatest = ml.version == Program.LatestVersion.Version.Replace("v", string.Empty);
            updateButton.Enabled = !isLatest;
            updateButton.Text = isLatest ? "Up-To-Date" : "Update";
        }

        public static LibraryGame CreateLibraryGame(Info info, bool askToInstall)
        {
            if (info == null)
                return null;
            var ml = MLInfo.GetInfo(Path.GetDirectoryName(info.path));
            if (ml == null)
            {
                if (askToInstall)
                {
                    // TODO: Open installer
                }
                return null;
            }

            return new LibraryGame(info, ml);
        }

        private void launchButton_Click(object sender, EventArgs e)
        {
            StartGame();
        }

        public void StartGame()
        {
            string steamid = Path.Combine(info.dir, "steam_appid.txt");
            if (File.Exists(steamid))
            {
                string id = File.ReadAllText(steamid);
                if (id.Length > 0)
                {
                    Process.Start("steam://rungameid/" + id);
                    return;
                }
            }
            Process.Start(info.path);
        }

        public void OpenMelonsMenu(bool plugins)
        {
            new Mods(this, plugins).ShowDialog();
        }

        public void OpenOptionsMenu()
        {
            new GameOptions(this).ShowDialog();
        }

        private void modsButton_Click(object sender, EventArgs e)
        {
            OpenMelonsMenu(false);
        }

        private void pluginsButton_Click(object sender, EventArgs e)
        {
            OpenMelonsMenu(true);
        }

        private void optionsButton_Click(object sender, EventArgs e)
        {
            OpenOptionsMenu();
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            Installer.Install(Program.LatestVersion.Version, info);
        }

        public class Info
        {
            public readonly string path;
            public readonly string dir;
            public readonly string name;
            public readonly string author;
            public readonly bool hasOfficialInfo;

            private Info(string gameExePath)
            {
                path = gameExePath;
                dir = Path.GetDirectoryName(path);
                name = Path.GetFileNameWithoutExtension(path);
                author = string.Empty;
                hasOfficialInfo = false;
                string appInfoPath = Path.Combine(dir, name + @"_Data\app.info");
                bool hasAppInfo = File.Exists(appInfoPath);
                if (hasAppInfo)
                {
                    string appInfo = File.ReadAllText(appInfoPath);
                    if (appInfo.Contains('\n'))
                    {
                        var info = appInfo.Split('\n');
                        bool hasAuthor = info[0].Length > 0;
                        bool hasName = info[1].Length > 0;
                        if (hasAuthor) 
                            author = info[0];
                        if (hasName) 
                            name = info[1];
                        if (hasName && hasAuthor) 
                            hasOfficialInfo = true;
                    }
                }
            }

            public static Info Create(string gameExePath)
            {
                if (!File.Exists(gameExePath))
                    return null;

                return new Info(gameExePath);
            }
        }
    }
}
