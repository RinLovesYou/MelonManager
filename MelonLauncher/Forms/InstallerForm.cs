using MetroFramework.Forms;
using System;
using System.Drawing;
using System.Linq;

namespace MelonLauncher.Forms
{
    public partial class InstallerForm : MetroForm
    {
        public LibraryGame.Info game;

        public InstallerForm(LibraryGame.Info game)
        {
            this.game = game;
            InitializeComponent();

            gameName.Text = game.name;
            gameName.Location = new Point((Size.Width - this.gameName.Size.Width) / 2, 7);

            mlVersionSelect.Items.AddRange(Program.releasesAPI.ReleasesTbl.Select(x => x.Version).Where(x => !(game.x86 && (x.StartsWith("v0.1") || x.StartsWith("v0.2")))).ToArray());
            mlVersionSelect.SelectedIndex = 0;
        }

        private void installButton_Click(object sender, EventArgs e)
        {
            Installer.Install(mlVersionSelect.SelectedItem.ToString(), game, true);
            MelonLauncherForm.instance.pages.SelectedIndex = 2;
            Close();
            Dispose();
        }
    }
}
