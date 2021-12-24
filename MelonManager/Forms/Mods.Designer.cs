namespace MelonManager.Forms
{
    partial class Mods
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.modsList = new System.Windows.Forms.ListBox();
            this.gameName = new MetroFramework.Controls.MetroLabel();
            this.modsLabel = new MetroFramework.Controls.MetroLabel();
            this.metroPanel2 = new MetroFramework.Controls.MetroPanel();
            this.removeButton = new System.Windows.Forms.Button();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.modAuthor = new MetroFramework.Controls.MetroLabel();
            this.modVersion = new MetroFramework.Controls.MetroLabel();
            this.modName = new MetroFramework.Controls.MetroLabel();
            this.browseDir = new System.Windows.Forms.Button();
            this.addMelonBtn = new System.Windows.Forms.Button();
            this.metroPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // modsList
            // 
            this.modsList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.modsList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.modsList.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.modsList.ForeColor = System.Drawing.Color.Silver;
            this.modsList.FormattingEnabled = true;
            this.modsList.Location = new System.Drawing.Point(12, 50);
            this.modsList.Name = "modsList";
            this.modsList.Size = new System.Drawing.Size(200, 312);
            this.modsList.TabIndex = 0;
            this.modsList.SelectedIndexChanged += new System.EventHandler(this.modsList_SelectedIndexChanged);
            // 
            // gameName
            // 
            this.gameName.AutoSize = true;
            this.gameName.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.gameName.Location = new System.Drawing.Point(183, 7);
            this.gameName.Name = "gameName";
            this.gameName.Size = new System.Drawing.Size(48, 19);
            this.gameName.TabIndex = 1;
            this.gameName.Text = "Game";
            this.gameName.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.gameName.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // modsLabel
            // 
            this.modsLabel.AutoSize = true;
            this.modsLabel.FontSize = MetroFramework.MetroLabelSize.Small;
            this.modsLabel.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.modsLabel.Location = new System.Drawing.Point(10, 32);
            this.modsLabel.Name = "modsLabel";
            this.modsLabel.Size = new System.Drawing.Size(87, 15);
            this.modsLabel.TabIndex = 2;
            this.modsLabel.Text = "Installed mods:";
            this.modsLabel.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // metroPanel2
            // 
            this.metroPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.metroPanel2.Controls.Add(this.removeButton);
            this.metroPanel2.Controls.Add(this.metroLabel3);
            this.metroPanel2.Controls.Add(this.metroLabel2);
            this.metroPanel2.Controls.Add(this.metroLabel1);
            this.metroPanel2.Controls.Add(this.modAuthor);
            this.metroPanel2.Controls.Add(this.modVersion);
            this.metroPanel2.Controls.Add(this.modName);
            this.metroPanel2.Enabled = false;
            this.metroPanel2.HorizontalScrollbarBarColor = true;
            this.metroPanel2.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel2.HorizontalScrollbarSize = 10;
            this.metroPanel2.Location = new System.Drawing.Point(218, 248);
            this.metroPanel2.Name = "metroPanel2";
            this.metroPanel2.Size = new System.Drawing.Size(243, 144);
            this.metroPanel2.TabIndex = 4;
            this.metroPanel2.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroPanel2.UseCustomBackColor = true;
            this.metroPanel2.VerticalScrollbarBarColor = true;
            this.metroPanel2.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel2.VerticalScrollbarSize = 10;
            // 
            // removeButton
            // 
            this.removeButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(10)))), ((int)(((byte)(30)))));
            this.removeButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.removeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.removeButton.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.removeButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.removeButton.Location = new System.Drawing.Point(172, 117);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(68, 24);
            this.removeButton.TabIndex = 8;
            this.removeButton.Text = "Remove";
            this.removeButton.UseVisualStyleBackColor = false;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.FontSize = MetroFramework.MetroLabelSize.Small;
            this.metroLabel3.Location = new System.Drawing.Point(4, 71);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(42, 15);
            this.metroLabel3.TabIndex = 7;
            this.metroLabel3.Text = "Author";
            this.metroLabel3.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroLabel3.UseCustomBackColor = true;
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.FontSize = MetroFramework.MetroLabelSize.Small;
            this.metroLabel2.Location = new System.Drawing.Point(4, 38);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(43, 15);
            this.metroLabel2.TabIndex = 6;
            this.metroLabel2.Text = "Version";
            this.metroLabel2.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroLabel2.UseCustomBackColor = true;
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.FontSize = MetroFramework.MetroLabelSize.Small;
            this.metroLabel1.Location = new System.Drawing.Point(4, 5);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(38, 15);
            this.metroLabel1.TabIndex = 5;
            this.metroLabel1.Text = "Name";
            this.metroLabel1.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroLabel1.UseCustomBackColor = true;
            // 
            // modAuthor
            // 
            this.modAuthor.AutoSize = true;
            this.modAuthor.FontSize = MetroFramework.MetroLabelSize.Small;
            this.modAuthor.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.modAuthor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.modAuthor.Location = new System.Drawing.Point(4, 86);
            this.modAuthor.Name = "modAuthor";
            this.modAuthor.Size = new System.Drawing.Size(10, 15);
            this.modAuthor.TabIndex = 4;
            this.modAuthor.Text = " ";
            this.modAuthor.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.modAuthor.UseCustomBackColor = true;
            this.modAuthor.UseCustomForeColor = true;
            // 
            // modVersion
            // 
            this.modVersion.AutoSize = true;
            this.modVersion.FontSize = MetroFramework.MetroLabelSize.Small;
            this.modVersion.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.modVersion.ForeColor = System.Drawing.Color.Gray;
            this.modVersion.Location = new System.Drawing.Point(4, 53);
            this.modVersion.Name = "modVersion";
            this.modVersion.Size = new System.Drawing.Size(10, 15);
            this.modVersion.TabIndex = 3;
            this.modVersion.Text = " ";
            this.modVersion.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.modVersion.UseCustomBackColor = true;
            this.modVersion.UseCustomForeColor = true;
            // 
            // modName
            // 
            this.modName.AutoSize = true;
            this.modName.FontSize = MetroFramework.MetroLabelSize.Small;
            this.modName.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.modName.ForeColor = System.Drawing.Color.Green;
            this.modName.Location = new System.Drawing.Point(4, 20);
            this.modName.Name = "modName";
            this.modName.Size = new System.Drawing.Size(10, 15);
            this.modName.TabIndex = 2;
            this.modName.Text = " ";
            this.modName.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.modName.UseCustomBackColor = true;
            this.modName.UseCustomForeColor = true;
            // 
            // browseDir
            // 
            this.browseDir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.browseDir.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.browseDir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.browseDir.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.browseDir.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.browseDir.Location = new System.Drawing.Point(218, 218);
            this.browseDir.Name = "browseDir";
            this.browseDir.Size = new System.Drawing.Size(107, 24);
            this.browseDir.TabIndex = 9;
            this.browseDir.Text = "Browse directory";
            this.browseDir.UseVisualStyleBackColor = false;
            this.browseDir.Click += new System.EventHandler(this.openExplorer_Click);
            // 
            // addMelonBtn
            // 
            this.addMelonBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.addMelonBtn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.addMelonBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addMelonBtn.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.addMelonBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.addMelonBtn.Location = new System.Drawing.Point(12, 368);
            this.addMelonBtn.Name = "addMelonBtn";
            this.addMelonBtn.Size = new System.Drawing.Size(200, 24);
            this.addMelonBtn.TabIndex = 10;
            this.addMelonBtn.Text = "Add Melon";
            this.addMelonBtn.UseVisualStyleBackColor = false;
            this.addMelonBtn.Click += new System.EventHandler(this.addMelonBtn_Click);
            // 
            // Mods
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 401);
            this.Controls.Add(this.addMelonBtn);
            this.Controls.Add(this.browseDir);
            this.Controls.Add(this.metroPanel2);
            this.Controls.Add(this.modsLabel);
            this.Controls.Add(this.gameName);
            this.Controls.Add(this.modsList);
            this.ForeColor = System.Drawing.Color.Silver;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Mods";
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.Style = MetroFramework.MetroColorStyle.Red;
            this.TextAlign = MetroFramework.Forms.MetroFormTextAlign.Center;
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.Load += new System.EventHandler(this.Mods_Load);
            this.metroPanel2.ResumeLayout(false);
            this.metroPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox modsList;
        private MetroFramework.Controls.MetroLabel gameName;
        private MetroFramework.Controls.MetroLabel modsLabel;
        private MetroFramework.Controls.MetroPanel metroPanel2;
        private MetroFramework.Controls.MetroLabel modAuthor;
        private MetroFramework.Controls.MetroLabel modVersion;
        private MetroFramework.Controls.MetroLabel modName;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.Button browseDir;
        private System.Windows.Forms.Button addMelonBtn;
    }
}