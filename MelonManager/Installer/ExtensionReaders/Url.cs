using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelonManager.Installer.ExtensionReaders
{
    public class Url : ExtensionReaderBase
    {
        public override string Extension => ".url";

        private static string ParseURL(string shortcut_path)
        {
            string[] file_lines = File.ReadAllLines(shortcut_path);
            if (file_lines.Length <= 0)
                return null;

            string urlstring = file_lines.First(x => (!string.IsNullOrEmpty(x) && x.StartsWith("URL=")));
            if (string.IsNullOrEmpty(urlstring))
                return null;

            urlstring = urlstring.Substring(4);
            if (string.IsNullOrEmpty(urlstring))
                return null;

            if (Steam.IsSteamURL(urlstring))
                return Steam.GetFilePathFromAppId(Steam.GetAppIdFromURL(urlstring));

            return null;
        }

        public override string Read(string path) => ParseURL(path);
    }
}
