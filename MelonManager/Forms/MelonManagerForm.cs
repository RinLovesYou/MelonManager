using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using MelonManager.Managers;
using MelonLoader.Managers;
using MetroFramework.Forms;
using System.Diagnostics;
using MelonLoader.URLs;
using System.Text;
using System.Collections.Generic;

namespace MelonManager.Forms
{
    public partial class MelonManagerForm : MetroForm
    {
        public static MelonManagerForm instance;
        public readonly string libFilePath = Path.Combine(Program.localFilesPath, "Library.cfg");
        public List<Task> tasks = new List<Task>();

        private string[] gamesPaths;

        public MelonManagerForm()
        {
            instance = this;
            InitializeComponent();
        }

        public LibraryGame GetLibGameByPath(string exePath)
        {
            var len = libraryPanel.Controls.Count;
            for (int a = 0; a < len; a++)
            {
                var gm = (LibraryGame)libraryPanel.Controls[a];
                if (gm.info.path == exePath)
                    return gm;
            }
            return null;
        }

        public void AddTask(Task task)
        {
            if (task == null)
                return;

            noTasksText.Visible = false;

            task.onFinishedCallback += () => FinishTask(task);
            task.onClose += () => CloseTask(task);
            task.onFailedCallback += (msg, ex) =>
            {
                FailTask(task);
            };
            tasks.Add(task);
            tasksLayoutPanel.Controls.Add(task);
            if (task.reliesOn != null)
                return;

            task.StartTask();
        }

        public void FailTask(Task task, bool close = false)
        {
            var len = tasksLayoutPanel.Controls.Count;
            for (int a = len - 1; a >= 0; a--)
            {
                var ctrl = (Task)tasksLayoutPanel.Controls[a];
                if (ctrl.reliesOn == task)
                    FailTask(ctrl);
            }
        }

        public void CloseTask(Task task, bool closeReliables = false)
        {
            tasksLayoutPanel.Controls.Remove(task);
            task.Dispose();

            if (closeReliables)
            {
                var len = tasksLayoutPanel.Controls.Count;
                for (int a = len - 1; a >= 0; a--)
                {
                    var ctrl = (Task)tasksLayoutPanel.Controls[a];
                    if (ctrl.reliesOn == task)
                        CloseTask(ctrl, closeReliables);
                }
            }

            if (tasksLayoutPanel.Controls.Count == 0)
                noTasksText.Visible = true;
        }

        public void FinishTask(Task task)
        {
            tasks.Remove(task);
            if (task.Failed)
                return;
            var len = tasksLayoutPanel.Controls.Count;
            for (int a = 0; a < len; a++)
            {
                var ctrl = (Task)tasksLayoutPanel.Controls[a];
                if (ctrl.reliesOn == task)
                    ctrl.StartTask();
            }
        }

        public void SaveLibrary()
        {
            Logger.Log("Saving library games...");
            var sb = new StringBuilder();

            for (int a = 0; a < libraryPanel.Controls.Count; a++)
            {
                var game = (LibraryGame)libraryPanel.Controls[a];
                sb.AppendLine(game.info.path);
            }

            File.WriteAllText(libFilePath, sb.ToString());
        }

        public bool AddLibraryGame(LibraryGame.Info gameInf, bool askToInstall)
        {
            var len = libraryPanel.Controls.Count;
            for (int a = 0; a < len; a++)
            {
                var g = libraryPanel.Controls[a];
                if (g is LibraryGame libGame && libGame.info.path == gameInf.path)
                {
                    Logger.Log($"Failed to add '{gameInf.path}' to the library, because an instance of it already exists.", Logger.Level.Warning);
                    return false;
                }
            }

            var game = LibraryGame.CreateLibraryGame(gameInf, askToInstall);
            if (game == null)
                return false;
            libraryPanel.Controls.Add(game);
            noLibGamesText.Visible = false;
            noLibGamesText2.Visible = false;
            Logger.Log($"Added '{gameInf.path}' to the library.");
            return true;
        }

        public void RemoveLibraryGame(LibraryGame game)
        {
            if (game == null)
                return;

            libraryPanel.Controls.Remove(game);
            if (libraryPanel.Controls.Count == 0)
            {
                noLibGamesText.Visible = true;
                noLibGamesText2.Visible = false;
            }
            Logger.Log($"Removed '{game.info.path}' from the library.");
            game.Dispose();
        }

        public void ClearLibrary()
        {
            libraryPanel.Controls.Clear();
            noLibGamesText.Visible = true;
            Logger.Log($"Cleared the library.");
        }


        #region UI Events
        private void MelonManager_Load(object sender, EventArgs e)
        {
            versionText.Text = 'v' + Application.ProductVersion;
            bool libCfgExists = File.Exists(libFilePath);
            if (!libCfgExists)
                File.Create(libFilePath).Dispose();
            string libs = libCfgExists ? File.ReadAllText(libFilePath) : string.Empty;
            gamesPaths = libs.Contains('\n') ? libs.Split('\n') : new string[] { libs };
            foreach (var lib in gamesPaths)
            {
                if (lib == string.Empty) continue;
                var info = LibraryGame.Info.Create(lib);
                if (info == null)
                    continue;
                AddLibraryGame(info, false);
            }

            consoleButton.Enabled = !Utils.IsConsoleOpen;

            updateMLCheck.Checked = Config.Values.autoUpdateML;
            updateMMCheck.Checked = Config.Values.autoUpdateMM;

            Logger.Log("Form loaded.");
        }

        private void updateBtn_Click(object sender, EventArgs e)
        {

        }

        private void addGameButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dia = new OpenFileDialog();
            dia.Filter = "Unity Game Executable | *.exe";
            dia.Title = "Select a Unity Game Executable";
            if (dia.ShowDialog() != DialogResult.OK) return;
            if (GetLibGameByPath(dia.FileName) != null)
            {
                CustomMessageBox.Error("This game is already in your library!");
                return;
            }
            var info = LibraryGame.Info.Create(dia.FileName);
            if (info == null)
                return;
            AddLibraryGame(info, true);
        }

        private void MelonManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            var tasksRunning = tasks.Count != 0;
            if (tasksRunning)
            {
                if (CustomMessageBox.Question("There are still tasks running in the background, closing the manager might cause unexpected issues.\nWould you like to close anyways?") != DialogResult.Yes)
                {
                    e.Cancel = true;
                    Logger.Log("Closing form aborted, tasks are still running");
                    return;
                }
                Logger.Log("Closing from while tasks are still running", Logger.Level.Warning);
            }
            TempPath.ClearTemp();
            SaveLibrary();
            Config.Save();
            Logger.Log("Closing");
        }

        private void consoleButton_Click(object sender, EventArgs e)
        {
            consoleButton.Enabled = false;
            Utils.OpenConsole();
            Console.Write(Logger.GetWholeLog());
            Logger.Log("You have opened the debug console. Everything written to this console gets saved to the latest log file.", Logger.Level.Warning);
        }

        private void localDataButton_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", $"\"{Program.localFilesPath}\"");
        }

        private void mlDiscordLink_Click(object sender, EventArgs e)
        {
            Process.Start(ExternalLinks.Discord);
        }

        private void mlWikiLink_Click(object sender, EventArgs e)
        {
            Process.Start(ExternalLinks.Wiki);
        }

        private void mlGithubLink_Click(object sender, EventArgs e)
        {
            Process.Start(ExternalLinks.MLGitHub);
        }

        private void lgTwitterLink_Click(object sender, EventArgs e)
        {
            Process.Start(ExternalLinks.Twitter);
        }

        private void lgGithubLink_Click(object sender, EventArgs e)
        {
            Process.Start(ExternalLinks.LavaGang);
        }

        private void mmGithubLink_Click(object sender, EventArgs e)
        {
            Process.Start(ExternalLinks.MMGitHub);
        }

        private void clearDataButton_Click(object sender, EventArgs e)
        {
            if (CustomMessageBox.Question("Are you sure you want to clear all data?\nThis includes all logs and configurations!") != DialogResult.Yes)
                return;

            Program.ClearData();
        }

        private void pages_SelectedIndexChanged(object sender, EventArgs e)
        {
            Config.Values.lastPage = pages.SelectedIndex;
        }

        private void updateMMCheck_CheckedChanged(object sender, EventArgs e)
        {
            Config.Values.autoUpdateMM = updateMMCheck.Checked;
        }

        private void updateMLCheck_CheckedChanged(object sender, EventArgs e)
        {
            Config.Values.autoUpdateML = updateMLCheck.Checked;
        }
        #endregion
    }
}
