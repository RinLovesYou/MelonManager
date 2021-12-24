
namespace MelonManager.Forms
{
    partial class InstallerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InstallerForm));
            this.metroPanel2 = new MetroFramework.Controls.MetroPanel();
            this.mlVersionSelect = new MetroFramework.Controls.MetroComboBox();
            this.mlVersion = new MetroFramework.Controls.MetroLabel();
            this.installButton = new System.Windows.Forms.Button();
            this.gameName = new MetroFramework.Controls.MetroLabel();
            this.metroPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // metroPanel2
            // 
            this.metroPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.metroPanel2.Controls.Add(this.mlVersionSelect);
            this.metroPanel2.Controls.Add(this.mlVersion);
            this.metroPanel2.HorizontalScrollbarBarColor = true;
            this.metroPanel2.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel2.HorizontalScrollbarSize = 10;
            this.metroPanel2.Location = new System.Drawing.Point(23, 29);
            this.metroPanel2.Name = "metroPanel2";
            this.metroPanel2.Size = new System.Drawing.Size(149, 57);
            this.metroPanel2.TabIndex = 14;
            this.metroPanel2.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroPanel2.UseCustomBackColor = true;
            this.metroPanel2.VerticalScrollbarBarColor = true;
            this.metroPanel2.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel2.VerticalScrollbarSize = 10;
            // 
            // mlVersionSelect
            // 
            this.mlVersionSelect.FontSize = MetroFramework.MetroComboBoxSize.Small;
            this.mlVersionSelect.FormattingEnabled = true;
            this.mlVersionSelect.ItemHeight = 19;
            this.mlVersionSelect.Location = new System.Drawing.Point(37, 25);
            this.mlVersionSelect.Name = "mlVersionSelect";
            this.mlVersionSelect.Size = new System.Drawing.Size(75, 25);
            this.mlVersionSelect.Style = MetroFramework.MetroColorStyle.Red;
            this.mlVersionSelect.TabIndex = 13;
            this.mlVersionSelect.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.mlVersionSelect.UseSelectable = true;
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
            // installButton
            // 
            this.installButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.installButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.installButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.installButton.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.installButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.installButton.Location = new System.Drawing.Point(23, 92);
            this.installButton.Name = "installButton";
            this.installButton.Size = new System.Drawing.Size(149, 24);
            this.installButton.TabIndex = 14;
            this.installButton.Text = "Install";
            this.installButton.UseVisualStyleBackColor = false;
            this.installButton.Click += new System.EventHandler(this.installButton_Click);
            // 
            // gameName
            // 
            this.gameName.AutoSize = true;
            this.gameName.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.gameName.Location = new System.Drawing.Point(72, 7);
            this.gameName.Name = "gameName";
            this.gameName.Size = new System.Drawing.Size(48, 19);
            this.gameName.TabIndex = 15;
            this.gameName.Text = "Game";
            this.gameName.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // InstallerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(195, 124);
            this.Controls.Add(this.gameName);
            this.Controls.Add(this.metroPanel2);
            this.Controls.Add(this.installButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InstallerForm";
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.Style = MetroFramework.MetroColorStyle.Red;
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.InstallerForm_FormClosing);
            this.metroPanel2.ResumeLayout(false);
            this.metroPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroPanel metroPanel2;
        private MetroFramework.Controls.MetroComboBox mlVersionSelect;
        private MetroFramework.Controls.MetroLabel mlVersion;
        private System.Windows.Forms.Button installButton;
        private MetroFramework.Controls.MetroLabel gameName;
    }
}