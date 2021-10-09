
namespace MelonLauncher.Forms
{
    partial class GameOptions
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
            this.gameName = new MetroFramework.Controls.MetroLabel();
            this.removeFromLibButton = new System.Windows.Forms.Button();
            this.uninstallButton = new System.Windows.Forms.Button();
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.mlVersion = new MetroFramework.Controls.MetroLabel();
            this.gamePath = new MetroFramework.Controls.MetroTextBox();
            this.mlVersionSelect = new MetroFramework.Controls.MetroComboBox();
            this.installButton = new System.Windows.Forms.Button();
            this.metroPanel2 = new MetroFramework.Controls.MetroPanel();
            this.metroPanel1.SuspendLayout();
            this.metroPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // gameName
            // 
            this.gameName.AutoSize = true;
            this.gameName.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.gameName.Location = new System.Drawing.Point(163, 7);
            this.gameName.Name = "gameName";
            this.gameName.Size = new System.Drawing.Size(48, 19);
            this.gameName.TabIndex = 2;
            this.gameName.Text = "Game";
            this.gameName.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.gameName.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // removeFromLibButton
            // 
            this.removeFromLibButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(10)))), ((int)(((byte)(30)))));
            this.removeFromLibButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.removeFromLibButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.removeFromLibButton.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.removeFromLibButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.removeFromLibButton.Location = new System.Drawing.Point(210, 451);
            this.removeFromLibButton.Name = "removeFromLibButton";
            this.removeFromLibButton.Size = new System.Drawing.Size(183, 24);
            this.removeFromLibButton.TabIndex = 9;
            this.removeFromLibButton.Text = "Remove from the Library";
            this.removeFromLibButton.UseVisualStyleBackColor = false;
            this.removeFromLibButton.Click += new System.EventHandler(this.removeFromLibButton_Click);
            // 
            // uninstallButton
            // 
            this.uninstallButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(10)))), ((int)(((byte)(30)))));
            this.uninstallButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.uninstallButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.uninstallButton.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.uninstallButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.uninstallButton.Location = new System.Drawing.Point(23, 451);
            this.uninstallButton.Name = "uninstallButton";
            this.uninstallButton.Size = new System.Drawing.Size(181, 24);
            this.uninstallButton.TabIndex = 10;
            this.uninstallButton.Text = "Uninstall MelonLoader";
            this.uninstallButton.UseVisualStyleBackColor = false;
            this.uninstallButton.Click += new System.EventHandler(this.uninstallButton_Click);
            // 
            // metroPanel1
            // 
            this.metroPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.metroPanel1.Controls.Add(this.gamePath);
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 10;
            this.metroPanel1.Location = new System.Drawing.Point(23, 398);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(370, 47);
            this.metroPanel1.TabIndex = 12;
            this.metroPanel1.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroPanel1.UseCustomBackColor = true;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 10;
            // 
            // mlVersion
            // 
            this.mlVersion.AutoSize = true;
            this.mlVersion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.mlVersion.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.mlVersion.Location = new System.Drawing.Point(6, 3);
            this.mlVersion.Name = "mlVersion";
            this.mlVersion.Size = new System.Drawing.Size(139, 19);
            this.mlVersion.TabIndex = 2;
            this.mlVersion.Text = "MelonLoader Version";
            this.mlVersion.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.mlVersion.UseCustomBackColor = true;
            // 
            // gamePath
            // 
            // 
            // 
            // 
            this.gamePath.CustomButton.Image = null;
            this.gamePath.CustomButton.Location = new System.Drawing.Point(303, 1);
            this.gamePath.CustomButton.Name = "";
            this.gamePath.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.gamePath.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.gamePath.CustomButton.TabIndex = 1;
            this.gamePath.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.gamePath.CustomButton.UseSelectable = true;
            this.gamePath.CustomButton.Visible = false;
            this.gamePath.Lines = new string[] {
        "path"};
            this.gamePath.Location = new System.Drawing.Point(22, 12);
            this.gamePath.MaxLength = 32767;
            this.gamePath.Name = "gamePath";
            this.gamePath.PasswordChar = '\0';
            this.gamePath.ReadOnly = true;
            this.gamePath.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.gamePath.SelectedText = "";
            this.gamePath.SelectionLength = 0;
            this.gamePath.SelectionStart = 0;
            this.gamePath.ShortcutsEnabled = true;
            this.gamePath.Size = new System.Drawing.Size(325, 23);
            this.gamePath.Style = MetroFramework.MetroColorStyle.Red;
            this.gamePath.TabIndex = 3;
            this.gamePath.Text = "path";
            this.gamePath.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.gamePath.UseSelectable = true;
            this.gamePath.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.gamePath.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // mlVersionSelect
            // 
            this.mlVersionSelect.FontSize = MetroFramework.MetroComboBoxSize.Small;
            this.mlVersionSelect.FormattingEnabled = true;
            this.mlVersionSelect.ItemHeight = 19;
            this.mlVersionSelect.Location = new System.Drawing.Point(11, 25);
            this.mlVersionSelect.Name = "mlVersionSelect";
            this.mlVersionSelect.Size = new System.Drawing.Size(75, 25);
            this.mlVersionSelect.Style = MetroFramework.MetroColorStyle.Red;
            this.mlVersionSelect.TabIndex = 13;
            this.mlVersionSelect.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.mlVersionSelect.UseSelectable = true;
            this.mlVersionSelect.SelectedIndexChanged += new System.EventHandler(this.mlVersionSelect_SelectedIndexChanged);
            // 
            // installButton
            // 
            this.installButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.installButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.installButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.installButton.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.installButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.installButton.Location = new System.Drawing.Point(92, 26);
            this.installButton.Name = "installButton";
            this.installButton.Size = new System.Drawing.Size(69, 24);
            this.installButton.TabIndex = 14;
            this.installButton.Text = "Reinstall";
            this.installButton.UseVisualStyleBackColor = false;
            this.installButton.Click += new System.EventHandler(this.installButton_Click);
            // 
            // metroPanel2
            // 
            this.metroPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.metroPanel2.Controls.Add(this.installButton);
            this.metroPanel2.Controls.Add(this.mlVersionSelect);
            this.metroPanel2.Controls.Add(this.mlVersion);
            this.metroPanel2.HorizontalScrollbarBarColor = true;
            this.metroPanel2.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel2.HorizontalScrollbarSize = 10;
            this.metroPanel2.Location = new System.Drawing.Point(23, 42);
            this.metroPanel2.Name = "metroPanel2";
            this.metroPanel2.Size = new System.Drawing.Size(171, 57);
            this.metroPanel2.TabIndex = 13;
            this.metroPanel2.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroPanel2.UseCustomBackColor = true;
            this.metroPanel2.VerticalScrollbarBarColor = true;
            this.metroPanel2.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel2.VerticalScrollbarSize = 10;
            // 
            // GameOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(416, 498);
            this.Controls.Add(this.metroPanel1);
            this.Controls.Add(this.uninstallButton);
            this.Controls.Add(this.removeFromLibButton);
            this.Controls.Add(this.gameName);
            this.Controls.Add(this.metroPanel2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GameOptions";
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.Style = MetroFramework.MetroColorStyle.Red;
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.TopMost = true;
            this.metroPanel1.ResumeLayout(false);
            this.metroPanel2.ResumeLayout(false);
            this.metroPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroLabel gameName;
        private System.Windows.Forms.Button removeFromLibButton;
        private System.Windows.Forms.Button uninstallButton;
        private MetroFramework.Controls.MetroPanel metroPanel1;
        private MetroFramework.Controls.MetroLabel mlVersion;
        private MetroFramework.Controls.MetroTextBox gamePath;
        private MetroFramework.Controls.MetroComboBox mlVersionSelect;
        private System.Windows.Forms.Button installButton;
        private MetroFramework.Controls.MetroPanel metroPanel2;
    }
}