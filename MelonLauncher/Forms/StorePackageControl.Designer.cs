
namespace MelonLauncher.Forms
{
    partial class StorePackageControl
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
            this.packageIcon = new System.Windows.Forms.PictureBox();
            this.packageNameText = new MetroFramework.Controls.MetroLabel();
            this.descriptionText = new MetroFramework.Controls.MetroLabel();
            this.installButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.packageIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // packageIcon
            // 
            this.packageIcon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.packageIcon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.packageIcon.ErrorImage = global::MelonLauncher.Properties.Resources.Melon_170;
            this.packageIcon.Image = global::MelonLauncher.Properties.Resources.Melon_170;
            this.packageIcon.Location = new System.Drawing.Point(44, 18);
            this.packageIcon.Name = "packageIcon";
            this.packageIcon.Size = new System.Drawing.Size(170, 170);
            this.packageIcon.TabIndex = 0;
            this.packageIcon.TabStop = false;
            // 
            // packageNameText
            // 
            this.packageNameText.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.packageNameText.AutoSize = true;
            this.packageNameText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.packageNameText.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.packageNameText.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.packageNameText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.packageNameText.Location = new System.Drawing.Point(61, 191);
            this.packageNameText.Name = "packageNameText";
            this.packageNameText.Size = new System.Drawing.Size(138, 25);
            this.packageNameText.TabIndex = 8;
            this.packageNameText.Text = "Package Name";
            this.packageNameText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.packageNameText.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.packageNameText.UseCustomBackColor = true;
            // 
            // descriptionText
            // 
            this.descriptionText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.descriptionText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.descriptionText.FontSize = MetroFramework.MetroLabelSize.Small;
            this.descriptionText.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.descriptionText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.descriptionText.Location = new System.Drawing.Point(20, 216);
            this.descriptionText.MaximumSize = new System.Drawing.Size(220, 0);
            this.descriptionText.Name = "descriptionText";
            this.descriptionText.Size = new System.Drawing.Size(220, 64);
            this.descriptionText.TabIndex = 9;
            this.descriptionText.Text = "Description";
            this.descriptionText.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.descriptionText.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.descriptionText.UseCustomBackColor = true;
            this.descriptionText.WrapToLine = true;
            // 
            // installButton
            // 
            this.installButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.installButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.installButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.installButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.installButton.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.installButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.installButton.Location = new System.Drawing.Point(3, 283);
            this.installButton.Name = "installButton";
            this.installButton.Size = new System.Drawing.Size(184, 26);
            this.installButton.TabIndex = 10;
            this.installButton.Text = "Install";
            this.installButton.UseVisualStyleBackColor = false;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.button1.Location = new System.Drawing.Point(193, 283);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(65, 26);
            this.button1.TabIndex = 11;
            this.button1.Text = "Info";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // StorePackageControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.Controls.Add(this.button1);
            this.Controls.Add(this.installButton);
            this.Controls.Add(this.descriptionText);
            this.Controls.Add(this.packageNameText);
            this.Controls.Add(this.packageIcon);
            this.Name = "StorePackageControl";
            this.Size = new System.Drawing.Size(261, 312);
            this.Style = MetroFramework.MetroColorStyle.Red;
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.UseCustomBackColor = true;
            ((System.ComponentModel.ISupportInitialize)(this.packageIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox packageIcon;
        private MetroFramework.Controls.MetroLabel packageNameText;
        private MetroFramework.Controls.MetroLabel descriptionText;
        private System.Windows.Forms.Button installButton;
        private System.Windows.Forms.Button button1;
    }
}
