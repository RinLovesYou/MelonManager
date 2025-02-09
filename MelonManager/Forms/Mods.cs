﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MelonManager.Games;
using MelonManager.Melons;
using MelonManager.Utils;
using MetroFramework.Forms;

namespace MelonManager.Forms
{
    public partial class Mods : MetroForm
    {
        private List<Melon> Melons => isEditingPlugins ? game.plugins : game.mods;
        public readonly LibraryGame game;
        private readonly bool isEditingPlugins;
        public Melon selectedMelon;

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
            var idx = modsList.SelectedIndex;

            modsList.Items.Clear();
            foreach (var m in Melons)
                modsList.Items.Add(m.name);

            if (idx < modsList.Items.Count)
                modsList.SelectedIndex = idx;

            InvalidMelons((isEditingPlugins ? game.invalidPlugins : game.invalidMods).Count);
            HighlightMelon(modsList.SelectedIndex == -1 ? null : Melons[modsList.SelectedIndex]);
        }

        private void InvalidMelons(int count)
        {
            var any = count != 0;

            invalidMelonsWarning.Visible = any;
            removeInvalidBtn.Visible = any;
            if (any)
            {
                invalidMelonsWarning.Text = $"Warning!\nThere {"is".Plural("are", count)} {count} incompatible or invalid {"Melon".Plural("Melons", count)}\nin your {(isEditingPlugins ? "plugins" : "mods")} folder.\n\nWould you like to remove {"it".Plural("them", count)}?";
            }
        }

        public void RemoveMelon()
        {
            if (selectedMelon == null)
                return;
            if (File.Exists(selectedMelon.path))
                File.Delete(selectedMelon.path);
            game.RefreshMelons(isEditingPlugins);
        }

        private void Mods_Load(object sender, EventArgs e)
        {
            game.RefreshMelons(isEditingPlugins);
        }

        public void HighlightMelon(Melon melon)
        {
            selectedMelon = melon;
            var isMelon = melon != null;
            metroPanel2.Enabled = isMelon;
            removeButton.Visible = isMelon;
            modName.Visible = isMelon;
            modVersion.Visible = isMelon;
            modAuthor.Visible = isMelon;
            if (!isMelon)
                return;
            modName.Text = melon.name;
            modVersion.Text = "v" + melon.version;
            modAuthor.Text = melon.author;
        }

        private void modsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Melon melon = modsList.SelectedIndex == -1 ? null : Melons[modsList.SelectedIndex];
            HighlightMelon(melon);
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            if (CustomMessageBox.Question($"Are you sure you want to remove '{(string)modsList.SelectedItem}'?") != DialogResult.Yes) 
                return;
            RemoveMelon();
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
                game.AddMelon(f, isEditingPlugins);

            MelonsRefreshed();
        }

        private void removeInvalidBtn_Click(object sender, EventArgs e)
        {
            game.RemoveInvalidMelons(isEditingPlugins);
        }
    }
}
