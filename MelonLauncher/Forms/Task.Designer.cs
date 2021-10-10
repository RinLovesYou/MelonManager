
namespace MelonLauncher.Forms
{
    partial class Task
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
            this.progressBar = new MetroFramework.Controls.MetroProgressBar();
            this.nameText = new MetroFramework.Controls.MetroLabel();
            this.closeButton = new System.Windows.Forms.Button();
            this.idText = new MetroFramework.Controls.MetroLabel();
            this.statusText = new MetroFramework.Controls.MetroLabel();
            this.statusText2 = new MetroFramework.Controls.MetroLabel();
            this.SuspendLayout();
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(3, 51);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(619, 14);
            this.progressBar.Style = MetroFramework.MetroColorStyle.Red;
            this.progressBar.TabIndex = 0;
            this.progressBar.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // nameText
            // 
            this.nameText.AutoSize = true;
            this.nameText.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.nameText.Location = new System.Drawing.Point(0, 4);
            this.nameText.Name = "nameText";
            this.nameText.Size = new System.Drawing.Size(79, 19);
            this.nameText.TabIndex = 1;
            this.nameText.Text = "Task name";
            this.nameText.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.nameText.UseCustomBackColor = true;
            // 
            // closeButton
            // 
            this.closeButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.closeButton.Enabled = false;
            this.closeButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.closeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeButton.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.closeButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.closeButton.Location = new System.Drawing.Point(600, 3);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(22, 22);
            this.closeButton.TabIndex = 4;
            this.closeButton.Text = "x";
            this.closeButton.UseVisualStyleBackColor = false;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // idText
            // 
            this.idText.AutoSize = true;
            this.idText.FontSize = MetroFramework.MetroLabelSize.Small;
            this.idText.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.idText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.idText.Location = new System.Drawing.Point(3, 23);
            this.idText.Name = "idText";
            this.idText.Size = new System.Drawing.Size(54, 15);
            this.idText.TabIndex = 5;
            this.idText.Text = "Task id: 0";
            this.idText.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.idText.UseCustomBackColor = true;
            this.idText.UseCustomForeColor = true;
            // 
            // statusText
            // 
            this.statusText.AutoSize = true;
            this.statusText.FontSize = MetroFramework.MetroLabelSize.Small;
            this.statusText.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.statusText.ForeColor = System.Drawing.Color.Silver;
            this.statusText.Location = new System.Drawing.Point(438, 33);
            this.statusText.Name = "statusText";
            this.statusText.Size = new System.Drawing.Size(66, 15);
            this.statusText.TabIndex = 6;
            this.statusText.Text = "Not started";
            this.statusText.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.statusText.UseCustomBackColor = true;
            this.statusText.UseCustomForeColor = true;
            // 
            // statusText2
            // 
            this.statusText2.AutoSize = true;
            this.statusText2.FontSize = MetroFramework.MetroLabelSize.Small;
            this.statusText2.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.statusText2.ForeColor = System.Drawing.Color.Silver;
            this.statusText2.Location = new System.Drawing.Point(396, 33);
            this.statusText2.Name = "statusText2";
            this.statusText2.Size = new System.Drawing.Size(45, 15);
            this.statusText2.TabIndex = 7;
            this.statusText2.Text = "Status:";
            this.statusText2.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.statusText2.UseCustomBackColor = true;
            this.statusText2.UseCustomForeColor = true;
            // 
            // Task
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.Controls.Add(this.statusText2);
            this.Controls.Add(this.statusText);
            this.Controls.Add(this.idText);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.nameText);
            this.Controls.Add(this.progressBar);
            this.Name = "Task";
            this.Size = new System.Drawing.Size(625, 68);
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.UseCustomBackColor = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroProgressBar progressBar;
        private MetroFramework.Controls.MetroLabel nameText;
        private System.Windows.Forms.Button closeButton;
        private MetroFramework.Controls.MetroLabel idText;
        private MetroFramework.Controls.MetroLabel statusText;
        private MetroFramework.Controls.MetroLabel statusText2;
    }
}
