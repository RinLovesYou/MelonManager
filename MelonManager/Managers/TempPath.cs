using System;
using System.IO;

namespace MelonManager.Managers
{
    public static class TempPath
    {
        public static readonly string tempFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), @"AppData\Local\Temp\" + BuildInfo.Name);
        static TempPath()
        {
            ClearTemp();
        }

        public static string CreateTempFile()
        {
            VerifyPath();
            var path = Path.Combine(tempFolder, Guid.NewGuid().ToString());
            File.Create(path).Dispose();
            return path;
        }

        public static void ClearTemp()
        {
            VerifyPath();
            foreach (var f in Directory.EnumerateFiles(tempFolder))
            {
                File.Delete(f);
            }
        }

        private static void VerifyPath()
        {
            Directory.CreateDirectory(tempFolder);
        }
    }
}
