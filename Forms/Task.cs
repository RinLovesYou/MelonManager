using MetroFramework.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MelonLauncher.Forms
{
    public partial class Task : MetroUserControl
    {
        private static int nextId = 0;

        public int ProgressBarPercentage
        {
            get => progressBar.Value;
            set
            {
                Invoke(new Action(() => { progressBar.Value = value; }));
            }
        }

        public readonly string name;
        public readonly int id;
        public readonly Task reliesOn;
        private readonly Action<Task> task;

        public Action onFinishedCallback;
        public Action onClose;
        public Action<string, Exception> onFailedCallback;
        public bool Started { get; private set; }
        public bool Finished { get; private set; }
        public bool Failed { get; private set; }

        public Task(Action<Task> task, string name, Task reliesOn = null)
        {
            InitializeComponent();
            this.task = task;
            this.name = name;
            nameText.Text = name;

            id = nextId;
            idText.Text = "Task id: " + id.ToString();
            nextId++;

            this.reliesOn = reliesOn;

            statusText.Text = reliesOn == null ? "Not started" : $"Waiting for task {reliesOn.id} to finish...";
        }

        public void StartTask()
        {
            Started = true;
            ProgressBarPercentage = 0;
            statusText.Text = "Running";
            System.Threading.Tasks.Task.Run(() => task.Invoke(this)).ConfigureAwait(false); // Running a task in a task ofc
        }

        public void FinishTask()
        {
            Invoke(new Action(() =>
            {
                Finished = true;
                ProgressBarPercentage = 100;
                statusText.Text = "Finished";
                onFinishedCallback?.Invoke();
                Done();
            }));
        }

        public void FailTask(string message, Exception exception = null)
        {
            Invoke(new Action(() =>
            {
                Failed = true;
                Finished = true;
                ProgressBarPercentage = 0;
                statusText.Text = message;
                onFailedCallback?.Invoke(message, exception);

                CustomMessageBox.Error(message);

                Done();
            }));
        }

        public void WaitForTask(Task task)
        {
            while (!task.Finished)
                Thread.Sleep(5);

            if (task.Failed)
                Thread.CurrentThread.Abort();
        }

        private void Done()
        {
            closeButton.Enabled = true;
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            onClose?.Invoke();
        }
    }
}
