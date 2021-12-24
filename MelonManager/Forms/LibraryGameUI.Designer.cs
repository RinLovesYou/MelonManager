namespace MelonManager.Forms
{
    partial class LibraryGameUI
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gameName = new MetroFramework.Controls.MetroLabel();
            this.modsButton = new System.Windows.Forms.Button();
            this.gameAuthor = new MetroFramework.Controls.MetroLabel();
            this.pluginsButton = new System.Windows.Forms.Button();
            this.MLInfoPanel = new MetroFramework.Controls.MetroPanel();
            this.updateButton = new System.Windows.Forms.Button();
            this.MLVersion = new MetroFramework.Controls.MetroLabel();
            this.optionsButton = new System.Windows.Forms.Button();
            this.gamePicture = new System.Windows.Forms.PictureBox();
            this.MLInfoPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gamePicture)).BeginInit();
            this.SuspendLayout();
            // 
            // gameName
            // 
            this.gameName.AutoSize = true;
            this.gameName.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.gameName.Location = new System.Drawing.Point(75, 16);
            this.gameName.Name = "gameName";
            this.gameName.Size = new System.Drawing.Size(92, 19);
            this.gameName.TabIndex = 1;
            this.gameName.Text = "Game Name";
            this.gameName.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.gameName.UseCustomBackColor = true;
            // 
            // modsButton
            // 
            this.modsButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.modsButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.modsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.modsButton.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.modsButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.modsButton.Location = new System.Drawing.Point(443, 6);
            this.modsButton.Name = "modsButton";
            this.modsButton.Size = new System.Drawing.Size(60, 26);
            this.modsButton.TabIndex = 3;
            this.modsButton.Text = "Mods";
            this.modsButton.UseVisualStyleBackColor = false;
            this.modsButton.Click += new System.EventHandler(this.modsButton_Click);
            // 
            // gameAuthor
            // 
            this.gameAuthor.AutoSize = true;
            this.gameAuthor.FontSize = MetroFramework.MetroLabelSize.Small;
            this.gameAuthor.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.gameAuthor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gameAuthor.Location = new System.Drawing.Point(80, 35);
            this.gameAuthor.Name = "gameAuthor";
            this.gameAuthor.Size = new System.Drawing.Size(78, 15);
            this.gameAuthor.TabIndex = 4;
            this.gameAuthor.Text = "Game Author";
            this.gameAuthor.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.gameAuthor.UseCustomBackColor = true;
            this.gameAuthor.UseCustomForeColor = true;
            // 
            // pluginsButton
            // 
            this.pluginsButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.pluginsButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pluginsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.pluginsButton.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.pluginsButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pluginsButton.Location = new System.Drawing.Point(443, 37);
            this.pluginsButton.Name = "pluginsButton";
            this.pluginsButton.Size = new System.Drawing.Size(60, 26);
            this.pluginsButton.TabIndex = 5;
            this.pluginsButton.Text = "Plugins";
            this.pluginsButton.UseVisualStyleBackColor = false;
            this.pluginsButton.Click += new System.EventHandler(this.pluginsButton_Click);
            // 
            // MLInfoPanel
            // 
            this.MLInfoPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(22)))), ((int)(((byte)(22)))));
            this.MLInfoPanel.Controls.Add(this.updateButton);
            this.MLInfoPanel.Controls.Add(this.MLVersion);
            this.MLInfoPanel.HorizontalScrollbarBarColor = true;
            this.MLInfoPanel.HorizontalScrollbarHighlightOnWheel = false;
            this.MLInfoPanel.HorizontalScrollbarSize = 10;
            this.MLInfoPanel.Location = new System.Drawing.Point(509, 4);
            this.MLInfoPanel.Name = "MLInfoPanel";
            this.MLInfoPanel.Size = new System.Drawing.Size(113, 62);
            this.MLInfoPanel.TabIndex = 7;
            this.MLInfoPanel.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.MLInfoPanel.UseCustomBackColor = true;
            this.MLInfoPanel.VerticalScrollbarBarColor = true;
            this.MLInfoPanel.VerticalScrollbarHighlightOnWheel = false;
            this.MLInfoPanel.VerticalScrollbarSize = 10;
            // 
            // updateButton
            // 
            this.updateButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.updateButton.Enabled = false;
            this.updateButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.updateButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.updateButton.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.updateButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.updateButton.Location = new System.Drawing.Point(3, 33);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(107, 26);
            this.updateButton.TabIndex = 8;
            this.updateButton.Text = "Installing";
            this.updateButton.UseVisualStyleBackColor = false;
            this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
            // 
            // MLVersion
            // 
            this.MLVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.MLVersion.AutoSize = true;
            this.MLVersion.FontSize = MetroFramework.MetroLabelSize.Small;
            this.MLVersion.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.MLVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.MLVersion.Location = new System.Drawing.Point(27, 8);
            this.MLVersion.Name = "MLVersion";
            this.MLVersion.Size = new System.Drawing.Size(57, 15);
            this.MLVersion.TabIndex = 8;
            this.MLVersion.Text = "ML v0.4.4";
            this.MLVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.MLVersion.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.MLVersion.UseCustomBackColor = true;
            this.MLVersion.UseCustomForeColor = true;
            // 
            // optionsButton
            // 
            this.optionsButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.optionsButton.BackgroundImage = global::MelonManager.Properties.Resources.Cog;
            this.optionsButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.optionsButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.optionsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.optionsButton.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.optionsButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.optionsButton.Location = new System.Drawing.Point(411, 6);
            this.optionsButton.Name = "optionsButton";
            this.optionsButton.Size = new System.Drawing.Size(26, 26);
            this.optionsButton.TabIndex = 6;
            this.optionsButton.UseVisualStyleBackColor = false;
            this.optionsButton.Click += new System.EventHandler(this.optionsButton_Click);
            // 
            // gamePicture
            // 
            this.gamePicture.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.gamePicture.Location = new System.Drawing.Point(4, 4);
            this.gamePicture.Name = "gamePicture";
            this.gamePicture.Size = new System.Drawing.Size(65, 61);
            this.gamePicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.gamePicture.TabIndex = 0;
            this.gamePicture.TabStop = false;
            // 
            // LibraryGameUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.Controls.Add(this.MLInfoPanel);
            this.Controls.Add(this.optionsButton);
            this.Controls.Add(this.pluginsButton);
            this.Controls.Add(this.gameAuthor);
            this.Controls.Add(this.modsButton);
            this.Controls.Add(this.gameName);
            this.Controls.Add(this.gamePicture);
            this.Name = "LibraryGameUI";
            this.Size = new System.Drawing.Size(625, 68);
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.UseCustomBackColor = true;
            this.MLInfoPanel.ResumeLayout(false);
            this.MLInfoPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gamePicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox gamePicture;
        private MetroFramework.Controls.MetroLabel gameName;
        private System.Windows.Forms.Button modsButton;
        private MetroFramework.Controls.MetroLabel gameAuthor;
        private System.Windows.Forms.Button pluginsButton;
        private System.Windows.Forms.Button optionsButton;
        private MetroFramework.Controls.MetroPanel MLInfoPanel;
        private MetroFramework.Controls.MetroLabel MLVersion;
        private System.Windows.Forms.Button updateButton;
    }
}
