using MelonLoader.Interfaces;
using MelonManager.Games;
using MelonManager.Utils;
using MetroFramework.Forms;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MelonManager.Forms
{
    public partial class GameOptions : MetroForm
    {
        private LibraryGame game;

        public GameOptions(LibraryGame game)
        {
            if (!Directory.Exists(game.info.dir)) 
                throw new DirectoryNotFoundException();
            InitializeComponent();
            gameName.Text = game.info.name;
            gameName.Location = new Point((Size.Width - this.gameName.Size.Width) / 2, 7);
            this.game = game;

            var currentVer = game.ml.version;
            mlVersionSelect.Items.AddRange(MelonLoaderGitHub.releasesTbl.Where(x => !(game.info.x86 && x.Windows_x86 == null)).Select(x => x.Version).ToArray());
            if (!mlVersionSelect.Items.Contains(currentVer))
            {
                mlVersionSelect.Items.Insert(0, currentVer);
                mlVersionSelect.SelectedIndex = 0;
                return;
            }
            mlVersionSelect.SelectedItem = currentVer;
        }

        private void installButton_Click(object sender, EventArgs e)
        {
            game.InstallML(mlVersionSelect.SelectedItem.ToString());
            Dispose();
        }

        private void mlVersionSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool enableInstall = MelonLoaderGitHub.LatestVersion != null && UtilsBox.CompareVersions(MelonLoaderGitHub.LatestVersion.Version, mlVersionSelect.SelectedItem.ToString()) != 2;
            installButton.Enabled = enableInstall;

            var ver =  UtilsBox.CompareVersions(mlVersionSelect.SelectedItem.ToString(), game.ml.version);
            switch (ver)
            {
                case 0:
                    installButton.Text = "Reinstall";
                    break;
                case 1:
                    installButton.Text = "Update";
                    break;
                case 2:
                    installButton.Text = "Downgrade";
                    break;
            }
        }

        private void removeFromLibButton_Click(object sender, EventArgs e)
        {
            if (CustomMessageBox.Question($"Are you sure you want to remove {game.info.name} from MelonManager's games library?\nMelonLoader will not be removed.") != DialogResult.Yes)
                return;

            game.Remove();
            Dispose();
        }

        private void uninstallButton_Click(object sender, EventArgs e)
        {
            if (CustomMessageBox.Question($"Are you sure you want to uninstall MelonLoader from {game.info.name}?") != DialogResult.Yes)
                return;

            game.UninstallML(true);
            Dispose();
        }
    }
}
