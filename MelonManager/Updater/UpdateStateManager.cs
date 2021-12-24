using MelonManager.Forms;
using MelonManager.Managers;
using MelonManager.Tasks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace MelonManager.Updater
{
    public static class UpdateStateManager
    {
        public static string path;

        public static void Start(string path)
        {
            Logger.Log("Initializing update state");
            UpdateStateManager.path = path;

            var task = new Task($"Updating MelonManager", InstallTask);
            MiniForm.RunTask(task);
            if (task.Failed)
                return;

            Process.Start(path, "-dontchecksingleinstance");
        }

        private static void InstallTask(Task task)
        {
            Task.Status = "Preparing for download";
            string uriString = Constants.SelfReleases + "/latest/download/MelonManager.exe";

            var mlTempPath = TempPath.CreateTempFile();
            task.onFinishedCallback.Subscribe((task) => File.Delete(mlTempPath));

            var wc = new WebClient();
            wc.DownloadProgressChanged += (sender, e) => Task.ProgressPercentage = e.ProgressPercentage;
            wc.DownloadFileCompleted += (sender, e) =>
            {
                wc.Dispose();
                if (e.Error != null)
                {
                    task.FailTask("Failed to download the latest version of " + BuildInfo.Name, e.Error);
                    return;
                }
                else if (e.Cancelled)
                {
                    task.FailTask($"The download of {BuildInfo.Name} has been cancelled.");
                    return;
                }

                InstallTaskCopy(task, mlTempPath);
            };

            Task.Status = "Downloading...";
            try
            {
                wc.DownloadFileAsync(new Uri(uriString), mlTempPath);
            }
            catch (Exception ex)
            {
                wc.Dispose();
                task.FailTask("Failed to download the latest version of " + BuildInfo.Name, ex);
                return;
            }
        }

        private static void InstallTaskCopy(Task task, string mlTempPath)
        {
            Task.Status = "Copying files";
            Program.KillOtherInstances();
            try
            {
                if (File.Exists(path))
                    File.Delete(path);

                File.Copy(mlTempPath, path);
            }
            catch
            {
                File.Copy(mlTempPath, path.Remove(path.Length - 4) + " " + new Random().Next(int.MaxValue).ToString()); // Lmao idk
            }

            task.FinishTask();
        }
    }
}
