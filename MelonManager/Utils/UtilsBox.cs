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

namespace MelonManager.Utils
{
    public static class UtilsBox
    {
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

        /// <returns>0 if versions are equal, 1 if <paramref name="versionString"/> is higher or 2 if <paramref name="versionString2"/> is higher.</returns>
        public static int CompareVersions(string versionString, string versionString2)
        {
            var b = 0;
            var c = 0;
            if (versionString[0] == 'v')
                b = 1;
            if (versionString2[0] == 'v')
                c = 1;

            var done1 = false;
            var done2 = false;
            var len1 = versionString.Length;
            var len2 = versionString2.Length;
            for (; ; )
            {
                var num1 = 0;
                if (!done1)
                {
                    for (; ; b++)
                    {
                        if (b >= len1)
                        {
                            done1 = true;
                            break;
                        }
                        var ch = versionString[b];
                        if (ch == '.')
                        {
                            b++;
                            break;
                        }
                        if (ch < '0' || ch > '9')
                        {
                            done1 = true;
                            break;
                        }
                        num1 *= 10;
                        num1 += ch - '0';
                    }
                }

                var num2 = 0;
                if (!done2)
                {
                    for (; ; c++)
                    {
                        if (c >= len2)
                        {
                            done2 = true;
                            break;
                        }
                        var ch = versionString2[c];
                        if (ch == '.')
                        {
                            c++;
                            break;
                        }
                        if (ch < '0' || ch > '9')
                        {
                            done2 = true;
                            break;
                        }
                        num2 *= 10;
                        num2 += ch - '0';
                    }
                }

                if (num1 != num2)
                    return num1 > num2 ? 1 : 2;

                if (done1 && done2)
                    return 0;
            }
        }
    }
}
