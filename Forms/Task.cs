using MelonLauncher.Managers;
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
            Log("Task started");

            Started = true;
            ProgressBarPercentage = 0;
            statusText.Text = "Running";
            System.Threading.Tasks.Task.Run(() => task.Invoke(this)).ConfigureAwait(false); // Running a task in a task ofc
        }

        public void FinishTask()
        {
            Log("Task finished");
            Invoke(new Action(() =>
            {
                statusText.Text = "Finished";
            }));
            Done();
        }

        public void FailTask(string message, Exception exception = null)
        {
            Log("Task failed: " + message, Logger.Level.Error);
            if (exception != null)
                Log("Task exception: " + exception.ToString(), Logger.Level.Error);
            Invoke(new Action(() =>
            {
                Failed = true;
                statusText.Text = message;
                onFailedCallback?.Invoke(message, exception);

                CustomMessageBox.Error(message);
            }));
            Done();
        }

        public void WaitForTask(Task task)
        {
            Log($"Waiting for task: '{task.name}' {task.id}");
            Invoke(new Action(() =>
            {
                statusText.Text = $"Waiting for task {task.id} to finish...";
            }));

            while (!task.Finished)
                Thread.Sleep(5);

            if (task.Failed)
            {
                FailTask($"Task {task.id} failed");
                Thread.CurrentThread.Abort();
                return;
            }

            Invoke(new Action(() =>
            {
                statusText.Text = "Running";
            }));
        }

        private void Done()
        {
            Invoke(new Action(() =>
            {
                Finished = true;
                ProgressBarPercentage = 100;
                onFinishedCallback?.Invoke();
                closeButton.Enabled = true;
            }));
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            onClose?.Invoke();
        }

        public void Log(string message, Logger.Level level = Logger.Level.Message)
        {
            Logger.Log($"['{name}' task {id}] {message}", level);
        }
    }
}
