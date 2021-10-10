using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;

namespace MelonLauncher.Forms
{
    public partial class CustomMessageBox : MetroForm
    {
        public CustomMessageBox(string text, bool isQuestion)
        {
            InitializeComponent();
            if (!isQuestion)
            {
                Style = MetroFramework.MetroColorStyle.Red;
                yesButton.Dispose();
                noButton.Text = "Ok";
            }
            message.Text = text;
            Size = new Size(message.Size.Width + 90, message.Size.Height + 125);
        }

        public static DialogResult Question(string question)
        {
            SystemSounds.Exclamation.Play();
            return new CustomMessageBox(question, true).ShowDialog();
        }

        public static void Error(string message)
        {
            SystemSounds.Hand.Play();
            new CustomMessageBox(message, false).ShowDialog();
        }

        private void noButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
        }

        private void yesButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
        }
    }
}
