using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using MelonLauncher.Forms;
using MelonLauncher.Managers;
using MelonLoader.Interfaces;

namespace MelonLauncher
{
    public static class Program
    {
        public static readonly string localFilesPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Lava Gang\MelonLauncher");

        internal static GitHub releasesAPI = new GitHub(MelonLoader.URLs.Repositories.MelonLoader);
        internal static GitHub.ReleaseData LatestMLVersion => releasesAPI.ReleasesTbl == null ? null : releasesAPI.ReleasesTbl.FirstOrDefault();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Directory.CreateDirectory(localFilesPath);

            AppDomain.CurrentDomain.UnhandledException += HandleException;
            releasesAPI.Refresh();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Forms.MelonLauncherForm());
        }

        private static void HandleException(object sender, UnhandledExceptionEventArgs e)
        {
            Logger.Log("[Unhandled Exception] " + e.ExceptionObject.ToString(), Logger.Level.Error);
            try
            {
                CustomMessageBox.Error("An unhandled exception has occured:\n\n" + e.ExceptionObject.ToString());
            }
            catch
            {
                MessageBox.Show(e.ExceptionObject.ToString(), "An Unhandled Exception Has Occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
