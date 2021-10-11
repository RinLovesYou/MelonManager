using MetroFramework.Forms;
using System;
using System.Drawing;
using System.Linq;

namespace MelonManager.Forms
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
            MelonManagerForm.instance.pages.SelectedIndex = 2;
            Close();
            Dispose();
        }
    }
}
