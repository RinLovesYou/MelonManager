using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using MelonManager.Forms;
using MelonManager.Managers;
using MelonLoader.Interfaces;
using System.Diagnostics;
using MelonLoader;
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

            var arg = args.FirstOrDefault(x => x.StartsWith("-update"));
            if (arg != null && arg.Length > 8)
            {
                UpdateStateManager.Start(arg[8..].Replace("\"", string.Empty));
                return;
            }

            if (!args.Contains("-dontchecksingleinstance"))
            {
                var inst = GetOtherInstances();
                if (inst.Count != 0)
                {
                    if (CustomMessageBox.Question($"There {"is an instance".Plural("are multiple instances", inst.Count)} of {BuildInfo.Name} already running,\nwould you like to close {"it".Plural("them", inst.Count)}?") != DialogResult.Yes)
                        return;
                    if (!inst.Kill())
                    {
                        CustomMessageBox.Error("Failed to kill an instance of " + BuildInfo.Name + "!\nTry doing it manually using the Task Manager or run" + BuildInfo.Name + " as admin.");
                        return;
                    }
                }
            }

            if (args.Contains("-console"))
                ConsoleUtils.OpenConsole();

            Directory.CreateDirectory(localFilesPath);
            Logger.Initialize();

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
            var tempCopy = Path.Combine(TempPath.tempFolder, BuildInfo.Name + ".exe");
            var currentProc = Process.GetCurrentProcess();
            var currentPath = currentProc.MainModule.FileName;
            File.Copy(currentPath, tempCopy);
            Application.Exit();
            Process.Start(tempCopy, $"-update \"{currentPath}\"");
        }

        public static List<Process> GetOtherInstances() // God, forgive me for this
        {
            var currentProc = Process.GetCurrentProcess();
            var id = currentProc.Id;
            var mainModPath = currentProc.MainModule.FileName;
            var mainInf = FileVersionInfo.GetVersionInfo(mainModPath);
            var name = mainInf.ProductName;
            var comp = mainInf.CompanyName;

            var procs = Process.GetProcesses();
            var result = new List<Process>();
            foreach (Process proc in procs)
            {
                try
                {
                    if (proc.Id == id)
                        continue;
                    var mod = proc.MainModule.FileName;
                    if (mainModPath == mod)
                    {
                        result.Add(proc);
                        continue;
                    }
                    if (mod.StartsWith("C:\\Windows"))
                        continue;
                    var inf = FileVersionInfo.GetVersionInfo(mod);
                    if (inf.ProductName == name && inf.CompanyName == comp)
                    {
                        result.Add(proc);
                        continue;
                    }
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
