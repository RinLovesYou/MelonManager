using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MelonManager.Installer
{
    public static class OpenUnityGameDialog
    {
        public static string Open()
        {
            return OpenDia(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
        }

        private static string OpenDia(string initPath)
        {
            var dia = new OpenFileDialog();
            dia.Title = "Open Unity Game";
            dia.Filter = "Unity Game | *.exe;*.lnk;*.url";
            dia.InitialDirectory = initPath;
            if (dia.ShowDialog() != DialogResult.OK)
                return null;

            var path = PathFinder.FindPathOfExe(dia.FileName);
            var ext = Path.GetExtension(path);
            if (ext == ".exe")
                return path;
            if (string.IsNullOrEmpty(ext) && Directory.Exists(path))
                return OpenDia(path);
            return null;
        }
    }
}
