using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using MelonLauncher.Managers;
using MelonLoader.Managers;
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

        private bool AddLibraryGame(LibraryGame.Info gameInf, bool askToInstall)
        {
            var len = libraryPanel.Controls.Count;
            for (int a = 0; a < len; a++)
            {
                var g = libraryPanel.Controls[a];
                if (g is LibraryGame libGame && libGame.info.path == gameInf.path)
                {
                    return false;
                }
            }

            var game = LibraryGame.CreateLibraryGame(gameInf, askToInstall);
            if (game == null)
                return false;
            libraryPanel.Controls.Add(game);
            return true;
        }

        public void RemoveLibraryGame(LibraryGame game)
        {
            if (game == null)
                return;

            libraryPanel.Controls.Remove(game);
            game.Dispose();
        }

        public void CreateLibraryGame(LibraryGame.Info game, bool askToInstall)
        {
            if (!AddLibraryGame(game, askToInstall))
                return;
            libraryFileWriter.Write(game.path + "\n");
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
                var info = LibraryGame.Info.Create(lib);
                if (info == null)
                    continue;
                AddLibraryGame(info, false);
            }
        }

        private void addGameButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dia = new OpenFileDialog();
            dia.Filter = "Unity Game Executable | *.exe";
            dia.Title = "Select a Unity Game Executable";
            if (dia.ShowDialog() != DialogResult.OK) return;
            var info = LibraryGame.Info.Create(dia.FileName);
            if (info == null)
                return;
            CreateLibraryGame(info, true);
        }

        private void MelonLauncher_FormClosing(object sender, FormClosingEventArgs e)
        {
            TempPath.ClearTemp();
            SaveLibrary();
            Config.Save();
        }
    }
}
