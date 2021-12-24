using MelonLoader.Interfaces;
using MelonManager.Games;
using MetroFramework.Forms;
using System;
using System.Drawing;
using System.Linq;

namespace MelonManager.Forms
{
    public partial class InstallerForm : MetroForm
    {
        public LibraryGame game;

        public InstallerForm(LibraryGame game)
        {
            this.game = game;
            InitializeComponent();

            gameName.Text = game.info.name;
            gameName.Location = new Point((Size.Width - this.gameName.Size.Width) / 2, 7);

            mlVersionSelect.Items.AddRange(GitHub.releasesTbl.Where(x => !(game.info.x86 && x.Windows_x86 == null)).Select(x => x.Version).ToArray());
            mlVersionSelect.SelectedIndex = 0;
        }

        private void installButton_Click(object sender, EventArgs e)
        {
            game.InstallML(mlVersionSelect.SelectedItem.ToString());
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Dispose();
        }

        private void InstallerForm_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            if (DialogResult == System.Windows.Forms.DialogResult.OK)
                return;

            if (CustomMessageBox.Question("To add a game to your library, MelonLoader must be installed.\nAre you sure you want to cancel the installation?") != System.Windows.Forms.DialogResult.Yes)
                e.Cancel = true;
        }
    }
}
