using MelonManager;
using MelonManager.Forms;
using MelonManager.Managers;
using System;
using System.IO;
using Tomlet;

namespace MelonLoader.Managers
{
    internal static class Config
    {
        private static string FilePath = Path.Combine(Program.localFilesPath, "MelonManager.cfg");
        public static FileValues Values = new FileValues();

        static Config()
        {
            Load();
        }

        internal static void Load()
        {
            if (!File.Exists(FilePath))
                return;
            string filestr = File.ReadAllText(FilePath);
            if (string.IsNullOrEmpty(filestr))
                return;
            try { Values = TomletMain.To<FileValues>(filestr); } catch { }
        }

        internal static void Save()
        {
            try
            {
                File.WriteAllText(FilePath, TomletMain.TomlStringFrom(Values));
            }
            catch (Exception ex)
            {
                Logger.Log("Failed to save config:\n" + ex.ToString(), Logger.Level.Error);
                CustomMessageBox.Error("Failed to save config:\n\n" + ex.ToString());
                return;
            }

            Logger.Log("Saved configs!");
        }

        public class FileValues
        {
            internal bool autoUpdateMM = true;
            internal bool autoUpdateML = true;
            internal int lastPage;
            internal string lastSelectedGamePath = null;
        }
    }
}
