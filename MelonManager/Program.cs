﻿using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using MelonManager.Forms;
using MelonManager.Managers;
using MelonLoader.Interfaces;

namespace MelonManager
{
    public static class Program
    {
        public static readonly string localFilesPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Lava Gang\MelonManager");

        internal static GitHub releasesAPI = new GitHub(MelonLoader.URLs.Repositories.MelonLoader);
        internal static GitHub.ReleaseData LatestMLVersion => releasesAPI.ReleasesTbl == null ? null : releasesAPI.ReleasesTbl.FirstOrDefault();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Contains("-console"))
                Utils.OpenConsole();

            Directory.CreateDirectory(localFilesPath);
            Logger.Initialize();

            AppDomain.CurrentDomain.UnhandledException += HandleException;
            releasesAPI.Refresh();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Forms.MelonManagerForm());
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
