using System;
using System.IO;

namespace MelonLauncher.Managers
{
    public static class Logger
    {
        public static string logsDir = Path.Combine(Program.localFilesPath, "Logs");
        public static string currentLogPath;
        public static string latestLogPath;
        private static StreamWriter latestLog;
        private static StreamWriter currentLog;
        private static object logLock = new object();

        static Logger()
        {
            latestLogPath = Path.Combine(Program.localFilesPath, "MelonLauncher_Latest.log");
            currentLogPath = Path.Combine(logsDir, "MelonLauncher_" + DateTime.Now.ToString("yy-MM-dd_HH-mm-ss.fff") + ".log");

            Directory.CreateDirectory(logsDir);

            latestLog = new StreamWriter(File.Open(latestLogPath, FileMode.Create, FileAccess.Write, FileShare.Read));
            currentLog = new StreamWriter(File.Open(currentLogPath, FileMode.Create, FileAccess.Write, FileShare.Read));
            latestLog.AutoFlush = true;
            currentLog.AutoFlush = true;

            Log("Timezone: " + TimeZone.CurrentTimeZone.StandardName);
            Log("Current date: " + DateTime.Now.ToString("yy-MM-dd"));
        }

        public static void Log(string message, Level level = Level.Message)
        {
            try
            {
                lock (logLock)
                {
                    var log = $"[{DateTime.Now.ToString("HH:mm:ss.fff")}] [{level}] " + message;
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
