using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MelonLauncher.Melons;
using MetroFramework.Forms;

namespace MelonLauncher.Forms
{
    public partial class Mods : MetroForm
    {
        private List<Melon> mods = new List<Melon>();
        public readonly LibraryGame game;
        public readonly string modsDir;
        private readonly bool isEditingPlugins;

        public Mods(LibraryGame game, bool isEditingPlugins)
        {
            if (!Directory.Exists(game.info.dir)) throw new DirectoryNotFoundException();
            this.isEditingPlugins = isEditingPlugins;
            InitializeComponent();
            gameName.Text = game.info.name;
            gameName.Location = new Point((Size.Width - this.gameName.Size.Width) / 2, 7);
            if (isEditingPlugins) modsLabel.Text = "Installed plugins:";
            modsDir = Path.Combine(game.info.dir, isEditingPlugins ? "Plugins" : "Mods");
            if (!Directory.Exists(modsDir)) Directory.CreateDirectory(modsDir);
            this.game = game;
        }

        public void AddMod(string path, Melon melon)
        {
            modsList.Items.Add(Path.GetFileName(path));
            mods.Add(melon);
        }

        public void RemoveMod(int index)
        {
            modsList.Items.RemoveAt(index);
            mods.RemoveAt(index);
            if (mods.Count > 0) modsList.SelectedIndex = index - 1;
            else 
            {
                metroPanel2.Enabled = false;
                modName.Text = string.Empty;
                modVersion.Text = string.Empty;
                modAuthor.Text = string.Empty;
            }
        }

        private void Mods_Load(object sender, EventArgs e)
        {
            var mods = Directory.GetFiles(modsDir, "*.dll");
            foreach (string mod in mods)
            {
                Melon melon;
                bool isCompatible = Melon.Open(mod, game, out melon);
                if (isCompatible)
                {
                    AddMod(mod, melon);
                }
            }
        }

        private void modsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (modsList.SelectedIndex < 0) return;
            Melon mod = mods[modsList.SelectedIndex];
            modName.Text = mod.name;
            modVersion.Text = "v" + mod.version;
            modAuthor.Text = mod.author;
            metroPanel2.Enabled = true;
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            if (CustomMessageBox.Question("Are you sure you want to remove \"" + (string)modsList.SelectedItem + "\"?") != DialogResult.Yes) return;
            try
            {
                File.Delete(Path.Combine(modsDir, (string)modsList.SelectedItem));
            }
            catch (Exception ex)
            {
                CustomMessageBox.Error(ex.Message);
                return;
            }
            RemoveMod(modsList.SelectedIndex);
        }

        private void openExplorer_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", modsDir);
        }
    }
}
