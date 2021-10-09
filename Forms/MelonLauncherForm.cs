using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MelonLauncher.Managers;
using MelonLoader.Managers;
using MetroFramework.Controls;
using MetroFramework.Forms;

namespace MelonLauncher.Forms
{
    public partial class MelonLauncherForm : MetroForm
    {
        public static MelonLauncherForm instance;

        private StreamWriter libraryFileWriter;
        private string[] gamesPaths;
        private StreamReader libraryFileReader;

        public MelonLauncherForm()
        {
            instance = this;
            InitializeComponent();
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
                Console.WriteLine(msg);
                if (ex != null)
                    Console.WriteLine(ex.ToString());
                FailTask(task);
            };
            tasksLayoutPanel.Controls.Add(task);
            if (task.reliesOn != null)
                return;

            task.StartTask();
        }

        public void FailTask(Task task)
        {
            var len = tasksLayoutPanel.Controls.Count;
            for (int a = len - 1; a >= 0; a--)
            {
                var ctrl = (Task)tasksLayoutPanel.Controls[a];
                if (ctrl.reliesOn == task)
                    CloseTask(ctrl, true);
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
                        CloseTask(ctrl);
                }
            }

            if (tasksLayoutPanel.Controls.Count == 0)
                noTasksText.Visible = true;
        }

        public void FinishTask(Task task)
        {
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
            libraryFileWriter.BaseStream.SetLength(0);
            libraryFileWriter.BaseStream.Position = 0;

            for (int a = 0; a < libraryPanel.Controls.Count; a++)
            {
                var ctrl = libraryPanel.Controls[a];
                if (ctrl is LibraryGame game)
                {
                    libraryFileWriter.WriteLine(game.info.path);
                }
            }

            libraryFileWriter.Flush();
        }

        private void AddLibraryGame(string path, bool askToInstall)
        {
            path = path.Replace("\r", "");

            var len = libraryPanel.Controls.Count;
            for (int a = 0; a < len; a++)
            {
                var g = libraryPanel.Controls[a];
                if (g is LibraryGame libGame && libGame.info.path == path)
                {
                    return;
                }
            }

            var game = LibraryGame.CreateLibraryGame(LibraryGame.Info.Create(path), askToInstall);
            if (game == null)
                return;
            libraryPanel.Controls.Add(game);
        }

        public void RemoveLibraryGame(LibraryGame game)
        {
            if (game == null)
                return;

            libraryPanel.Controls.Remove(game);
            game.Dispose();
        }

        public void CreateLibraryGame(string path, bool askToInstall)
        {
            libraryFileWriter.Write(path + "\n");
            AddLibraryGame(path, askToInstall);
        }

        private void MelonLauncher_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists(Program.localFilesPath))
                Directory.CreateDirectory(Program.localFilesPath);
            string libFilePath = Path.Combine(Program.localFilesPath, "Library");
            var stream = File.Open(libFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            libraryFileWriter = new StreamWriter(stream);
            libraryFileReader = new StreamReader(stream);
            string libs = libraryFileReader.ReadToEnd();
            gamesPaths = libs.Contains('\n') ? libs.Split('\n') : new string[] { libs };
            foreach (var lib in gamesPaths)
            {
                if (lib == string.Empty) continue;
                AddLibraryGame(lib, false);
            }
        }

        private void addGameButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dia = new OpenFileDialog();
            dia.Filter = "Unity Game Executable | *.exe";
            dia.Title = "Select a Unity Game Executable";
            if (dia.ShowDialog() != DialogResult.OK) return;
            CreateLibraryGame(dia.FileName, true);
        }

        private void MelonLauncher_FormClosing(object sender, FormClosingEventArgs e)
        {
            TempPath.ClearTemp();
            SaveLibrary();
            Config.Save();
        }
    }
}
