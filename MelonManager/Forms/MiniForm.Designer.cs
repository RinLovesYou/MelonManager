
namespace MelonManager.Forms
{
    partial class MiniForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MiniForm));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.metroProgressBar1 = new MetroFramework.Controls.MetroProgressBar();
            this.taskName = new MetroFramework.Controls.MetroLabel();
            this.taskStatus = new MetroFramework.Controls.MetroLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::MelonManager.Properties.Resources.Melon_170;
            this.pictureBox1.Location = new System.Drawing.Point(165, 10);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(170, 170);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // metroProgressBar1
            // 
            this.metroProgressBar1.Location = new System.Drawing.Point(23, 239);
            this.metroProgressBar1.Name = "metroProgressBar1";
            this.metroProgressBar1.Size = new System.Drawing.Size(454, 28);
            this.metroProgressBar1.Style = MetroFramework.MetroColorStyle.Red;
            this.metroProgressBar1.TabIndex = 1;
            this.metroProgressBar1.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // taskName
            // 
            this.taskName.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.taskName.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.taskName.Location = new System.Drawing.Point(23, 183);
            this.taskName.Name = "taskName";
            this.taskName.Size = new System.Drawing.Size(454, 25);
            this.taskName.TabIndex = 2;
            this.taskName.Text = "Task Name";
            this.taskName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.taskName.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // taskStatus
            // 
            this.taskStatus.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.taskStatus.Location = new System.Drawing.Point(23, 208);
            this.taskStatus.Name = "taskStatus";
            this.taskStatus.Size = new System.Drawing.Size(454, 28);
            this.taskStatus.TabIndex = 3;
            this.taskStatus.Text = "Task Status";
            this.taskStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.taskStatus.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // MiniForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 285);
            this.Controls.Add(this.taskStatus);
            this.Controls.Add(this.taskName);
            this.Controls.Add(this.metroProgressBar1);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MiniForm";
            this.Resizable = false;
            this.Style = MetroFramework.MetroColorStyle.Green;
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MiniForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private MetroFramework.Controls.MetroProgressBar metroProgressBar1;
        private MetroFramework.Controls.MetroLabel taskName;
        private MetroFramework.Controls.MetroLabel taskStatus;
    }
}