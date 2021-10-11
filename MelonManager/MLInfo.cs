using System.Diagnostics;
using System.IO;

namespace MelonManager
{
    public class MLInfo
    {
        public readonly string version;

        public MLInfo(string version)
        {
            this.version = version;
        }

        public static MLInfo GetInfo(string gameDirPath)
        {
            var mlDir = Path.Combine(gameDirPath, "MelonLoader");
            if (!Directory.Exists(mlDir))
                return null;

            var ml = Path.Combine(mlDir, "MelonLoader.ModHandler.dll");
            if (!File.Exists(ml))
            {
                ml = Path.Combine(mlDir, "MelonLoader.dll");
                if (!File.Exists(ml))
                    return null;
            }

            var ver = FileVersionInfo.GetVersionInfo(ml);

            return new MLInfo(ver.FileVersion);
        }
    }
}
