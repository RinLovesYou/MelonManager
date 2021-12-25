using MelonManager.Managers;
using MelonManager.Utils;
using System;
using System.Threading;

namespace MelonManager.Tasks
{
    public class Task
    {
        #region Statics
        private static int nextId = 0;
        public static ThreadSafeList<Task> tasksQueue = new ThreadSafeList<Task>();
        public static Task CurrentTask { get; private set; }
        public static FormsSafeEvent<Task> onTaskStarted = new FormsSafeEvent<Task>();
        public static FormsSafeEvent<Task> onTaskFinished = new FormsSafeEvent<Task>();
        public static FormsSafeEvent<Task, string, Exception> onTaskFailed = new FormsSafeEvent<Task, string, Exception>();
        public static FormsSafeEvent onProgressPercentageUpdate = new FormsSafeEvent();
        public static FormsSafeEvent onStatusUpdate = new FormsSafeEvent();
        private static int progressPercentage = 0;
        private static string status;

        public static int ProgressPercentage
        {
            get => progressPercentage;
            set
            {
                if (value == progressPercentage)
                    return;

                progressPercentage = value;
                onProgressPercentageUpdate.Invoke();
            }
        }

        public static string Status
        {
            get => status;
            set
            {
                if (value == status)
                    return;

                if (!string.IsNullOrEmpty(value))
                    CurrentTask.Log($"Status updated: '{value}'");
                status = value;
                onStatusUpdate.Invoke();
            }
        }

        static Task()
        {
            onTaskFinished.Subscribe(OnTaskFinished);
        }

        private static void OnTaskFinished(Task task)
        {
            CurrentTask = null;
            CheckNextTask();
        }

        private static void CheckNextTask()
        {
            if (CurrentTask != null || tasksQueue.Count == 0)
                return;

            var task = tasksQueue[0];
            StartTask(task);
        }

        private static void AddTask(Task task)
        {
            task.Log("Added to queue");
            if (CurrentTask == null)
            {
                StartTask(task);
                return;
            }
            lock (tasksQueue)
            {
                tasksQueue.Insert(0, task);
            }
        }

        private static void StartTask(Task task)
        {
            if (CurrentTask != null)
                return;

            CurrentTask = task;
            RemoveTask(task);
            task.InternalStartTask();
        }

        private static void RemoveTask(Task task)
        {
            tasksQueue.Remove(task);
        }
        #endregion

        public readonly string name;
        public readonly int id;
        private readonly Action<Task> task;
        private bool started;
        private bool init;
        private Thread thread;

        public FormsSafeEvent<Task> onFinishedCallback = new FormsSafeEvent<Task>();
        public bool Finished { get; private set; }
        public bool Failed { get; private set; }

        public Task(string name, Action<Task> task)
        {
            this.task = task;
            this.name = name;
            id = nextId;
            nextId++;
        }

        public void Init()
        {
            if (init)
                return;

            init = true;
            AddTask(this);
        }

        private void InternalStartTask()
        {
            if (started)
                return;

            Log("Task started");
            ProgressPercentage = 0;
            started = true;
            onTaskStarted.Invoke(this);
            thread = new Thread(ThreadEntryFunc);
            thread.Start();
        }

        private void ThreadEntryFunc()
        {
            try
            {
                task.Invoke(this);
            }
            catch (Exception ex)
            {
                FailTask("An unhandled exception has occured", ex);
            }
        }

        public void FinishTask()
        {
            Log("Task finished");
            Done();
        }

        public void FailTask(string message, Exception exception = null)
        {
            Log("Task failed: " + message, Logger.Level.Error);
            if (exception != null)
                Log("Task exception: " + exception.ToString(), Logger.Level.Error);
            Failed = true;
            onTaskFailed.Invoke(this, message, exception);
            //statusText.Text = message;

            //if (log)
            //CustomMessageBox.Error(message);

            Done();
        }

        private void Done()
        {
            Finished = true;
            ProgressPercentage = 0;
            Task.Status = string.Empty;
            RemoveTask(this);
            onFinishedCallback.Invoke(this);
            onTaskFinished.Invoke(this);

            if (thread.IsAlive)
                thread.Abort();
        }

        public void Log(string message, Logger.Level level = Logger.Level.Message)
        {
            Logger.Log($"['{name}' task {id}] {message}", level);
        }
    }
}
