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

            var currentVer = "v" + game.ml.version;
            mlVersionSelect.Items.AddRange(Program.releasesAPI.ReleasesTbl.Where(x => !(game.info.x86 && (x.Version.StartsWith("v0.1") || x.Version.StartsWith("v0.2")))).Select(x => x.Version).ToArray());
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
            Installer.Install(mlVersionSelect.SelectedItem.ToString(), game.info, false);
            MelonManagerForm.instance.pages.SelectedIndex = 2;
            Close();
            Dispose();
        }

        private void mlVersionSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool enableInstall = Program.LatestMLVersion != null && Utils.CompareVersions(Program.LatestMLVersion.Version, mlVersionSelect.SelectedItem.ToString()) != 2;
            installButton.Enabled = enableInstall;

            var ver =  Utils.CompareVersions(mlVersionSelect.SelectedItem.ToString(), game.ml.version);
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

            MelonManagerForm.instance.RemoveLibraryGame(game);
            Close();
            Dispose();
        }

        private void uninstallButton_Click(object sender, EventArgs e)
        {
            if (CustomMessageBox.Question($"Are you sure you want to uninstall MelonLoader from {game.info.name}?") != DialogResult.Yes)
                return;

            MelonManagerForm.instance.RemoveLibraryGame(game);
            Installer.Uninstall(game.info);
            Close();
            Dispose();
        }
    }
}
