using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MelonLauncher.Forms
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
            gamePath.Text = game.info.path;

            var currentVer = "v" + game.ml.version;
            mlVersionSelect.Items.AddRange(Program.releasesAPI.ReleasesTbl.Select(x => x.Version).ToArray());
            if (!mlVersionSelect.Items.Contains(currentVer))
                mlVersionSelect.Items.Add(currentVer);
            mlVersionSelect.SelectedItem = currentVer;
        }

        private void installButton_Click(object sender, EventArgs e)
        {
            Installer.Install(mlVersionSelect.SelectedItem.ToString(), game.info);
            MelonLauncherForm.instance.metroTabControl1.SelectedIndex = 2;
            Close();
            Dispose();
        }

        private void mlVersionSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idx = mlVersionSelect.SelectedIndex;

            bool enableInstall = Program.releasesAPI.ReleasesTbl.Count != 0 && idx >= 0 && idx < Program.releasesAPI.ReleasesTbl.Count;
            installButton.Enabled = enableInstall;

            bool reinstall = enableInstall && Program.releasesAPI.ReleasesTbl[mlVersionSelect.SelectedIndex].Version == "v" + game.ml.version;
            installButton.Text = reinstall ? "Reinstall" : "Install";
        }

        private void removeFromLibButton_Click(object sender, EventArgs e)
        {
            if (CustomMessageBox.Question($"Are you sure you want to remove {game.info.name} from MelonLauncher's games library?\nMelonLoader will not be removed.") != DialogResult.Yes)
                return;

            MelonLauncherForm.instance.RemoveLibraryGame(game);
            Close();
            Dispose();
        }

        private void uninstallButton_Click(object sender, EventArgs e)
        {
            // TODO: Uninstall ML
        }
    }
}
