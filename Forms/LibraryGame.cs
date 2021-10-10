using System;
using System.Drawing;
using System.Linq;
using MetroFramework.Controls;
using System.IO;
using System.Diagnostics;
using MelonLauncher.Managers;

namespace MelonLauncher.Forms
{
    public partial class LibraryGame : MetroUserControl
    {
        public Info info;
        public MLInfo ml;

        private LibraryGame(Info info)
        {
            this.info = info;
            InitializeComponent();
            gameName.Text = info.name;
            gameAuthor.Text = info.author;
            gamePicture.Image = Icon.ExtractAssociatedIcon(info.path).ToBitmap();

            VerifyVersion();
        }

        public void UpdatingStarted()
        {
            updateButton.Text = "Updating...";
            updateButton.Enabled = false;
            launchButton.Enabled = false;
            optionsButton.Enabled = false;
            modsButton.Enabled = false;
            pluginsButton.Enabled = false;
        }

        public void UpdatingFinished()
        {
            launchButton.Enabled = true;
            optionsButton.Enabled = true;
            modsButton.Enabled = true;
            pluginsButton.Enabled = true;
            VerifyVersion();
        }

        public void VerifyVersion()
        {
            ml = MLInfo.GetInfo(info.dir);
            if (ml == null)
            {
                MelonLauncherForm.instance.RemoveLibraryGame(this);
                CustomMessageBox.Error($"{info.name} had to be removed from the library because it's missing MelonLoader.\nReinstall MelonLoader on it to add it back.");
                return;
            }
            MLVersion.Text = "ML v" + ml.version;

            if (Program.LatestMLVersion == null)
            {
                updateButton.Enabled = false;
                updateButton.Text = "Updater Offline";
                return;
            }
            bool isLatest = ml.version == Program.LatestMLVersion.Version.Replace("v", string.Empty);
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
                    new InstallerForm(info).ShowDialog();
                }
                return null;
            }

            return new LibraryGame(info);
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
            Installer.Install(Program.LatestMLVersion.Version, info, false);
        }

        public class Info
        {
            public readonly string path;
            public readonly string dir;
            public readonly string name;
            public readonly string author;
            public bool x86;
            public readonly bool hasOfficialInfo;

            private Info(string gameExePath)
            {
                path = gameExePath;
                dir = Path.GetDirectoryName(path);
                name = Path.GetFileNameWithoutExtension(path);
                author = string.Empty;
                {
                    byte[] array = File.ReadAllBytes(path);
                    x86 = array == null || array.Length == 0 || BitConverter.ToUInt16(array, BitConverter.ToInt32(array, 60) + 4) != 34404;
                }
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
                gameExePath = gameExePath.Replace("\r", string.Empty);
                if (!File.Exists(gameExePath))
                {
                    Logger.Log($"Failed to create Info for '{gameExePath}' because it doesn't exist!", Logger.Level.Error);
                    return null;
                }
                var dir = Path.GetDirectoryName(gameExePath);
                if (!File.Exists(Path.Combine(dir, "UnityPlayer.dll")) || !Directory.Exists(Path.Combine(dir, gameExePath.Remove(gameExePath.Length - 4) + "_Data")))
                {
                    Logger.Log($"Failed to create Info for '{gameExePath}' because it isn't a unity game!", Logger.Level.Error);
                    return null;
                }

                return new Info(gameExePath);
            }

            public static bool operator ==(Info inf1, Info inf2)
            {
                if ((object)inf1 == null || (object)inf2 == null)
                    return (object)inf1 == (object)inf2; // Ik this looks stupid but its actually ok lmao

                return inf1.path == inf2.path;
            }

            public static bool operator !=(Info inf1, Info inf2)
            {
                if ((object)inf1 == null || (object)inf2 == null)
                    return (object)inf1 != (object)inf2; // Ik this looks stupid but its actually ok lmao

                return inf1.path != inf2.path;
            }
        }
    }
}
