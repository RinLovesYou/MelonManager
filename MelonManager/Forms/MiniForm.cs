using MelonManager.Tasks;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MelonManager.Forms
{
    public partial class MiniForm : MetroForm
    {
        private MiniForm(Task task)
        {
            InitializeComponent();
            taskName.Text = task.name;
            taskStatus.Text = Task.Status;
            Task.onStatusUpdate.Subscribe(StatusUpdate, this);
            Task.onProgressPercentageUpdate.Subscribe(ProgressUpdate, this);
            Task.onTaskFailed.Subscribe(Fail, this);
            task.onFinishedCallback.Subscribe(Finish, this);
            task.Init();
        }

        private void Fail(Task task, string message, Exception ex)
        {
            var msg = $"Task '{task.name}' failed: '{message}'";
            if (ex != null)
                msg += $"\n\n{ex}";
            CustomMessageBox.Error(msg);
        }

        private void Finish(Task task)
        {
            Dispose();
        }

        private void ProgressUpdate()
        {
            metroProgressBar1.Value = Task.ProgressPercentage;
        }

        private void StatusUpdate()
        {
            taskStatus.Text = Task.Status;
        }

        public static void RunTask(Task task)
        {
            var form = new MiniForm(task);
            Application.Run(form);
        }

        private void MiniForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }
    }
}
