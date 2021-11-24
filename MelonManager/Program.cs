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

namespace MelonManager
{
    public static class Program
    {
        public static readonly string localFilesPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Lava Gang\" + BuildInfo.Name);

        internal static GitHub releasesAPI = new GitHub(MelonLoader.URLs.Repositories.MelonLoader);
        internal static GitHub.ReleaseData LatestMLVersion => releasesAPI.ReleasesTbl == null ? null : releasesAPI.ReleasesTbl.FirstOrDefault();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var inst = CheckSingleInstance();
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

            if (args.Contains("-console"))
                Utils.OpenConsole();

            Directory.CreateDirectory(localFilesPath);
            Logger.Initialize();

            AppDomain.CurrentDomain.UnhandledException += (s, ex) => HandleException((Exception)ex.ExceptionObject);
            Application.ThreadException += (s, ex) => HandleException(ex.Exception);
            releasesAPI.Refresh();

            Application.Run(new Forms.MelonManagerForm());
        }

        public static List<Process> CheckSingleInstance()
        {
            var currentProc = Process.GetCurrentProcess();
            var mainModPath = currentProc.MainModule.FileName;
            var mainInf = FileVersionInfo.GetVersionInfo(mainModPath);

            var procs = Process.GetProcessesByName(currentProc.ProcessName);
            var result = new List<Process>();
            foreach (Process proc in procs)
            {
                try
                {
                    if (proc.Id == currentProc.Id)
                        continue;
                    var mod = proc.MainModule.FileName;
                    if (mainModPath == mod)
                    {
                        result.Add(proc);
                        continue;
                    }
                    var inf = FileVersionInfo.GetVersionInfo(mod);
                    if (inf.CompanyName == mainInf.CompanyName)
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
            Utils.TryClearDirectory(localFilesPath);
        }
    }
}
