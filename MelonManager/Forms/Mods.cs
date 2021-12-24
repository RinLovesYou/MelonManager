using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MelonManager.Games;
using MelonManager.Melons;
using MetroFramework.Forms;

namespace MelonManager.Forms
{
    public partial class Mods : MetroForm
    {
        private List<Melon> Melons => isEditingPlugins ? game.plugins : game.mods;
        public readonly LibraryGame game;
        private readonly bool isEditingPlugins;

        public Mods(LibraryGame game, bool isEditingPlugins)
        {
            this.game = game;
            this.isEditingPlugins = isEditingPlugins;
            InitializeComponent();
            gameName.Text = game.info.name;
            gameName.Location = new Point((Size.Width - this.gameName.Size.Width) / 2, 7);
            if (isEditingPlugins) 
                modsLabel.Text = "Installed plugins:";

            game.onMelonsRefreshed.Subscribe(MelonsRefreshed, this);
        }

        private void MelonsRefreshed()
        {
            modsList.Items.Clear();
            foreach (var m in Melons)
                modsList.Items.Add(m.name);
        }

        public void RemoveMelon(int index)
        {
            var melon = Melons[index];
            if (File.Exists(melon.path))
                File.Delete(melon.path);
            game.RefreshMelons();
        }

        private void Mods_Load(object sender, EventArgs e)
        {
            game.RefreshMelons();
        }

        public void HighlightMelon(Melon melon)
        {
            metroPanel2.Enabled = melon != null;
            modName.Visible = melon != null;
            modVersion.Visible = melon != null;
            modAuthor.Visible = melon != null;
            if (melon == null)
                return;
            modName.Text = melon.name;
            modVersion.Text = "v" + melon.version;
            modAuthor.Text = melon.author;
        }

        private void modsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (modsList.SelectedIndex < 0) 
                return;
            Melon melon = Melons[modsList.SelectedIndex];
            HighlightMelon(melon);
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            if (CustomMessageBox.Question($"Are you sure you want to remove '{(string)modsList.SelectedItem}'?") != DialogResult.Yes) 
                return;
            RemoveMelon(modsList.SelectedIndex);
        }

        private void openExplorer_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", Path.Combine(game.info.dir, isEditingPlugins ? "Plugins" : "Mods"));
        }

        private void addMelonBtn_Click(object sender, EventArgs e)
        {
            var dia = new OpenFileDialog();
            dia.Title = $"Open a Melon {(isEditingPlugins ? "Plugin" : "Mod")}";
            dia.Filter = "Melons | *.dll";
            dia.Multiselect = true;
            if (dia.ShowDialog() != DialogResult.OK)
                return;

            foreach (var f in dia.FileNames)
                game.VerifyMelon(f, isEditingPlugins, true);
        }
    }
}
