using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using MelonManager.Managers;
using MelonLoader.Managers;
using MetroFramework.Forms;
using System.Diagnostics;
using MelonLoader;
using System.Text;
using System.Collections.Generic;
using System.Threading;
using MelonManager.Tasks;
using MelonManager.Games;
using MelonManager.Utils;
using System.Drawing;
using MelonManager.Installer;

namespace MelonManager.Forms
{
    public partial class MelonManagerForm : MetroForm
    {
        public static MelonManagerForm instance;

        public MelonManagerForm()
        {
            InitializeComponent();
            instance = this;
        }

        public void AddLibraryGameUI(LibraryGameUI game)
        {
            libraryPanel.Controls.Add(game);
            noLibGamesText.Visible = false;
            noLibGamesText2.Visible = false;
        }

        public void RemoveLibraryGameUI(LibraryGameUI game)
        {
            libraryPanel.Controls.Remove(game);
            if (libraryPanel.Controls.Count == 0)
            {
                noLibGamesText.Visible = true;
                noLibGamesText2.Visible = true;
            }
        }

        public void ToggleTaskState(bool enabled)
        {
            Size = new Size(Size.Width, enabled ? 770 : 685);
        }

        private void TaskProgressUpdate()
        {
            taskProgressBar.Value = Task.ProgressPercentage;
        }

        private void TaskStatusUpdate()
        {
            taskStatus.Text = Task.Status;
        }

        private void TaskEnd(Task task)
        {
            if (Task.tasksQueue.Count != 0 || (Task.CurrentTask != null && Task.CurrentTask != task))
                return;

            ToggleTaskState(false);
        }

        private void TaskStart(Task task)
        {
            taskName.Text = task.name;
            ToggleTaskState(true);
        }

        private void CreateLibGameUI(LibraryGame game)
        {
            new LibraryGameUI(game);
        }


        #region UI Events
        private void MelonManager_Load(object sender, EventArgs e)
        {
            versionText.Text = 'v' + Application.ProductVersion;
            consoleButton.Enabled = !ConsoleUtils.IsConsoleOpen;
            updateMMCheck.Checked = Program.config.config.autoUpdate;
            pages.SelectedIndex = Program.config.config.mainFormPageIndex;

            ToggleTaskState(Task.CurrentTask != null);
            TaskProgressUpdate();
            TaskStatusUpdate();
            if (Task.CurrentTask != null)
                taskName.Text = Task.CurrentTask.name;
            Task.onTaskStarted.Subscribe(TaskStart, this);
            Task.onStatusUpdate.Subscribe(TaskStatusUpdate, this);
            Task.onProgressPercentageUpdate.Subscribe(TaskProgressUpdate, this);
            Task.onTaskFinished.Subscribe(TaskEnd, this);
            foreach (var g in LibraryGame.LibraryGames)
                CreateLibGameUI(g);
            LibraryGame.onGameAdded.Subscribe(CreateLibGameUI, this);

            Logger.Log("Form loaded.");
        }

        private void updateBtn_Click(object sender, EventArgs e)
        {

        }

        private void addGameButton_Click(object sender, EventArgs e)
        {
            var path = OpenUnityGameDialog.Open();
            if (path == null)
                return;
            if (LibraryGame.GetLibGame(path) != null)
            {
                CustomMessageBox.Error("This game already exists in your library!");
                return;
            }
            var info = GameInfo.Create(path);
            if (info == null)
                return;
            var g = LibraryGame.AddGame(info, false);
            if (g.ml == null && new InstallerForm(g).ShowDialog() != DialogResult.OK)
            {
                g.Remove();
                return;
            }
        }

        private void MelonManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            var tasksRunning = Task.CurrentTask != null;
            if (tasksRunning)
            {
                if (CustomMessageBox.Question("There are still tasks running in the background, closing the manager might cause unexpected issues.\nWould you like to close anyways?") != DialogResult.Yes)
                {
                    e.Cancel = true;
                    Logger.Log("Closing form aborted, tasks are still running");
                    return;
                }
                Logger.Log("Closing form while tasks are still running", Logger.Level.Warning);
            }
            Logger.Log("Closing form");

            Program.config.config.autoUpdate = updateMMCheck.Checked;
            Program.config.config.mainFormPageIndex = pages.SelectedIndex;
        }

        private void consoleButton_Click(object sender, EventArgs e)
        {
            consoleButton.Enabled = false;
            ConsoleUtils.OpenConsole();
            Console.Write(Logger.GetWholeLog());
            Logger.Log("You have opened the debug console. Everything written to this console gets saved to the latest log file.", Logger.Level.Warning);
        }

        private void localDataButton_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", $"\"{Program.localFilesPath}\"");
        }

        private void mlDiscordLink_Click(object sender, EventArgs e)
        {
            Process.Start(Constants.Discord);
        }

        private void mlWikiLink_Click(object sender, EventArgs e)
        {
            Process.Start(Constants.Wiki);
        }

        private void mlGithubLink_Click(object sender, EventArgs e)
        {
            Process.Start(Constants.MLGitHub);
        }

        private void lgTwitterLink_Click(object sender, EventArgs e)
        {
            Process.Start(Constants.Twitter);
        }

        private void lgGithubLink_Click(object sender, EventArgs e)
        {
            Process.Start(Constants.LavaGang);
        }

        private void mmGithubLink_Click(object sender, EventArgs e)
        {
            Process.Start(Constants.MMGitHub);
        }

        private void clearDataButton_Click(object sender, EventArgs e)
        {
            if (CustomMessageBox.Question("Are you sure you want to clear all data?\nThis includes all logs and configurations!") != DialogResult.Yes)
                return;

            Program.ClearData();
        }

        private void slidyDevLink_Click(object sender, EventArgs e)
        {
            Process.Start(Constants.SlidyDevGit);
        }

        private void samboyLink_Click(object sender, EventArgs e)
        {
            Process.Start(Constants.SamboyGit);
        }

        private void lavaGangLink_Click(object sender, EventArgs e)
        {
            Process.Start(Constants.LavaGang);
        }
        #endregion
    }
}
