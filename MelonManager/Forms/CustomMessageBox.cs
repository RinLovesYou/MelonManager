﻿using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;
using MetroFramework;
using MetroFramework.Forms;

namespace MelonManager.Forms
{
    public partial class CustomMessageBox : MetroForm
    {
        public CustomMessageBox(string text, bool isQuestion, MetroColorStyle color = MetroColorStyle.Green)
        {
            InitializeComponent();
            if (!isQuestion)
            {
                Style = color;
                yesButton.Dispose();
                noButton.Text = "Ok";
            }
            message.Text = text;
            Size = new Size(message.Size.Width + 90, message.Size.Height + 125);
        }

        public static DialogResult Question(string question)
        {
            SystemSounds.Exclamation.Play();
            return new CustomMessageBox(question, true, MetroColorStyle.Yellow).ShowDialog();
        }

        public static void Error(string message)
        {
            SystemSounds.Hand.Play();
            new CustomMessageBox(message, false, MetroColorStyle.Red).ShowDialog();
        }

        public static void Ok(string message)
        {
            SystemSounds.Exclamation.Play();
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
