using System;
using System.IO;
using System.Windows.Forms;

namespace MelonManager.Managers
{
    public static class Logger
    {
        public static string logsDir = Path.Combine(Program.localFilesPath, "Logs");
        public static string currentLogPath;
        public static string latestLogPath;
        public static StreamWriter latestLog;
        public static StreamWriter currentLog;
        private static object logLock = new object();
        private static bool initialized;

        public static void Initialize()
        {
            if (initialized)
                return;

            latestLogPath = Path.Combine(Program.localFilesPath, "MelonManager_Latest.log");
            currentLogPath = Path.Combine(logsDir, "MelonManager_" + DateTime.Now.ToString("yy-MM-dd_HH-mm-ss.fff") + ".log");

            Directory.CreateDirectory(logsDir);

            latestLog = new StreamWriter(File.Open(latestLogPath, FileMode.Create, FileAccess.Write, FileShare.Read));
            currentLog = new StreamWriter(File.Open(currentLogPath, FileMode.Create, FileAccess.Write, FileShare.Read));
            latestLog.AutoFlush = true;
            currentLog.AutoFlush = true;
            initialized = true;

            Log("============================================");
            Log("OS: " + Environment.OSVersion.ToString());
            Log("App version: v" + Application.ProductVersion);
            Log("Timezone: " + TimeZone.CurrentTimeZone.StandardName);
            Log("Current date: " + DateTime.Now.ToString("yy-MM-dd"));
            Log("============================================");
        }

        public static string GetWholeLog()
        {
            if (!initialized)
                return string.Empty;

            lock (logLock)
            {
                latestLog.Dispose();
                string str = File.ReadAllText(latestLogPath);
                latestLog = new StreamWriter(File.Open(latestLogPath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read));
                latestLog.AutoFlush = true;
                return str;
            }
        }

        public static void Log(string message, Level level = Level.Message)
        {
            if (!initialized)
                return;

            try
            {
                lock (logLock)
                {
                    var log = $"[{DateTime.Now.ToString("HH:mm:ss.fff")}] [{level}] " + message;
                    if (Utils.IsConsoleOpen)
                        Console.WriteLine(log);
                    latestLog.WriteLine(log);
                    currentLog.WriteLine(log);
                }
            }
            catch { }
        }

        public enum Level
        {
            Message,
            Warning,
            Error
        }
    }
}
