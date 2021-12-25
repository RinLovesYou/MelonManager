using System;
using System.Drawing;
using MetroFramework.Controls;
using MelonManager.Games;
using MelonManager.Utils;
using MelonLoader.Interfaces;

namespace MelonManager.Forms
{
    public partial class LibraryGameUI : MetroUserControl
    {
        public readonly LibraryGame game;

        public LibraryGameUI(LibraryGame game)
        {
            this.game = game;
            game.onMlVersionChange.Subscribe(UpdateMLVersion, this);
            game.onRemoved.Subscribe(Dispose, this);
            game.onInstallingStateChanged.Subscribe(InstallingStateChanged, this);
            Disposed += (s, e) => MelonManagerForm.instance.RemoveLibraryGameUI(this);
            InitializeComponent();

            gameName.Text = game.info.name;
            gameAuthor.Text = game.info.author;
            MelonManagerForm.instance.AddLibraryGameUI(this);
            InstallingStateChanged();

            var icon = Icon.ExtractAssociatedIcon(game.info.path);
            var iconBm = icon.ToBitmap();
            gamePicture.Image = iconBm;
        }

        private void InstallingStateChanged()
        {
            var ver = game.Installing || game.ml == null ? -1 : (GitHub.LatestVersion == null ? -1 : UtilsBox.CompareVersions(GitHub.LatestVersion.Version, game.ml.version)); // Forgive me for this
            updateButton.Enabled = ver > 0;
            updateButton.Text = game.Installing ? "Installing" : (ver == 0 ? "Up-To-Date" : (ver == -1 ? "Offline" : "Update"));
            var enabled = !game.Installing && game.ml != null;
            modsButton.Enabled = enabled;
            pluginsButton.Enabled = enabled;
            optionsButton.Enabled = enabled;
            UpdateMLVersion();
        }

        private void SetVersionText(string text)
        {
            MLVersion.Text = text;
            MLVersion.Location = new Point((MLInfoPanel.Size.Width - MLVersion.Size.Width) / 2, MLVersion.Location.Y);
        }

        private void UpdateMLVersion()
        {
            SetVersionText(game.Installing ? "Installing" : (game.ml == null ? "ML Not Installed" : "ML " + game.ml.version));
        }

        public void OpenMelonsMenu(bool plugins)
        {
            new Mods(game, plugins).ShowDialog();
        }

        public void OpenOptionsMenu()
        {
            new GameOptions(game).ShowDialog();
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
            game.InstallML(GitHub.LatestVersion.Version);
        }
    }
}
