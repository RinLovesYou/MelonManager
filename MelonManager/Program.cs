using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using MelonManager.Forms;
using MelonManager.Managers;
using MelonLoader.Interfaces;
using System.Diagnostics;
using System.Collections.Generic;
using MelonLoader.Managers;
using MelonManager.Games;
using MelonManager.Utils;
using MelonManager.Configs;
using MelonManager.Updater;

namespace MelonManager
{
    public static class Program
    {
        public static readonly string localFilesPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Lava Gang\" + BuildInfo.Name);
        public static readonly Config<MainConfig> config = new Config<MainConfig>("Main");
        public static bool latestVersion;

        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var currentProc = Process.GetCurrentProcess();
            var currentPath = currentProc.MainModule.FileName;

            var arg = Array.IndexOf(args, "-update");
            if (arg != -1 && arg + 1 < args.Length)
            {
                KillOtherInstances();
                Logger.Initialize();
                UpdateStateManager.Start(args[arg + 1]);
                return;
            }

            if (Path.GetFileNameWithoutExtension(currentPath) != BuildInfo.Name)
            {
                CustomMessageBox.Error($"Cannot start {BuildInfo.Name} with a different file name.\nRenaming {BuildInfo.Name}'s executable might cause unexpected issues in the future.\nPlease rename it back to '{BuildInfo.Name}'.");
                return;
            }

            if (args.Contains("-killotherinstances"))
            {
                KillOtherInstances();
            }
            else
            {
                var inst = GetOtherInstances();
                if (inst.Count != 0)
                {
                    CustomMessageBox.Error($"Cannot start another instance of {BuildInfo.Name} while another one is still running.");
                    return;
                }
            }

            Directory.CreateDirectory(localFilesPath);

            var ver = GitHub.GetLatestManagerVersion();
            if (ver != string.Empty)
            {
                var verComp = UtilsBox.CompareVersions(BuildInfo.Version, ver);

                latestVersion = verComp != 2;
            }
            else
                latestVersion = true;

            if (!latestVersion && config.config.autoUpdate)
            {
                Update();
                return;
            }

            AppDomain.CurrentDomain.UnhandledException += (s, ex) => HandleException((Exception)ex.ExceptionObject);
            Application.ThreadException += (s, ex) => HandleException(ex.Exception);
            GitHub.Refresh();

            Application.Run(new MelonManagerForm());

            TempPath.ClearTemp();
            LibraryGame.SaveLibrary();
            config.Save();
        }

        public static void KillOtherInstances()
        {
            GetOtherInstances().Kill();
        }

        public static void Update()
        {
            var currentProc = Process.GetCurrentProcess();
            var tempCopy = Path.Combine(TempPath.tempFolder, BuildInfo.Name + ".exe");
            var currentPath = currentProc.MainModule.FileName;
            File.Copy(currentPath, tempCopy);
            Application.Exit();
            Process.Start(tempCopy, $"-update \"{currentPath}\"");
        }

        public static List<Process> GetOtherInstances()
        {
            var currentProc = Process.GetCurrentProcess();
            var id = currentProc.Id;

            var procs = Process.GetProcessesByName(BuildInfo.Name);
            var result = new List<Process>();
            foreach (Process proc in procs)
            {
                try
                {
                    if (proc.Id == id)
                        continue;
                    result.Add(proc);
                }
                catch { }
            }
            return result;
        }

        private static void HandleException(Exception e)
        {
            Logger.Log("[Unhandled Exception] " + e.ToString(), Logger.Level.Error);
            try
            {
                CustomMessageBox.Error("An unhandled exception has occured:\n\n" + e.ToString());
            }
            catch
            {
                MessageBox.Show(e.ToString(), "An Unhandled Exception Has Occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void ClearData()
        {
            Application.Exit();
            Logger.Deinitialize();
            Directory.Delete(localFilesPath, true);
        }
    }
}
