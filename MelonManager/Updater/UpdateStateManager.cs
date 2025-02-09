﻿using MelonManager.Forms;
using MelonManager.Managers;
using MelonManager.Tasks;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;

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

            Process.Start(path, "-killotherinstances");
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
            catch (Exception ex)
            {
                task.FailTask($"Failed to copy the new version of {BuildInfo.Name} to the path of the old version.", ex);
                return;
            }

            task.FinishTask();
        }
    }
}
