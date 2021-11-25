using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MelonManager.Melons;
using MetroFramework.Forms;

namespace MelonManager.Forms
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

        public void AddMod(Melon melon)
        {
            var name = Path.GetFileName(melon.path);
            var newPath = Path.Combine(modsDir, name);
            if (!File.Exists(newPath))
                File.Copy(melon.path, newPath);

            modsList.Items.Add(melon.name);
            mods.Add(melon);
        }

        public void RemoveMod(int index)
        {
            var melon = mods[index];
            if (File.Exists(melon.path))
                File.Delete(melon.path);

            modsList.Items.RemoveAt(index);
            mods.RemoveAt(index);
            if (mods.Count > 0) 
                modsList.SelectedIndex = index - 1;
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
                bool isCompatible = Melon.Open(mod, game, false, out Melon melon);
                if (isCompatible)
                {
                    AddMod(melon);
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
            if (CustomMessageBox.Question("Are you sure you want to remove \"" + (string)modsList.SelectedItem + "\"?") != DialogResult.Yes) 
                return;
            RemoveMod(modsList.SelectedIndex);
        }

        private void openExplorer_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", modsDir);
        }

        private void addMelonBtn_Click(object sender, EventArgs e)
        {
            var dia = new OpenFileDialog();
            dia.Title = $"Open a {(isEditingPlugins ? "Plugin" : "Mod")} Melon";
            dia.Filter = "Melons | *.dll";
            if (dia.ShowDialog() != DialogResult.OK)
                return;

            if (!Melon.Open(dia.FileName, game, true, out Melon melon))
                return;

            AddMod(melon);
        }
    }
}
