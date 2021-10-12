using MelonManager.Managers;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MelonManager
{
    public static class Utils
    {
        private const int STD_OUTPUT_HANDLE = -11;
        private const int CODE_PAGE = 437;

        [DllImport("kernel32.dll",
               SetLastError = true,
               CharSet = CharSet.Auto,
               CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll",
            SetLastError = true,
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        private static extern int AllocConsole();

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        public static bool IsConsoleOpen { get; private set; }

        public static void OpenConsole()
        {
            if (IsConsoleOpen)
                return;

            AllocConsole();
            IntPtr stdHandle = GetStdHandle(STD_OUTPUT_HANDLE);
            SafeFileHandle safeFileHandle = new SafeFileHandle(stdHandle, true);
            FileStream fileStream = new FileStream(safeFileHandle, FileAccess.Write);
            Encoding encoding = Encoding.GetEncoding(CODE_PAGE);
            StreamWriter standardOutput = new StreamWriter(fileStream, encoding);
            standardOutput.AutoFlush = true;
            Console.SetOut(standardOutput);
            IsConsoleOpen = true;
        }

        public static bool Kill(this IEnumerable<Process> procs)
        {
            bool result = true;
            foreach (var p in procs)
            {
                try
                {
                    p.Kill();
                }
                catch { result = false; }
            }
            return result;
        }
    }
}
