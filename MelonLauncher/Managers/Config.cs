using MelonLauncher;
using System;
using System.IO;
using Tomlet;

namespace MelonLoader.Managers
{
    internal static class Config
    {
        private static string FilePath = Path.Combine(Program.localFilesPath, "MelonLauncher.cfg");
        private static FileValues Values = new FileValues();

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
            catch { }
        }

        private class FileValues
        {
            static FileValues()
            {
                Load();
            }

            internal bool autoUpdate = true;
            internal string lastSelectedGamePath = null;
        }
    }
}
