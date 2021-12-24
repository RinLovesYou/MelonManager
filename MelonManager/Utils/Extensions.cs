using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelonManager.Utils
{
    internal static class Extensions
    {
        internal static string Plural(this string str, string pluralForm, int count)
        {
            if (string.IsNullOrEmpty(str))
                return str;
            if (Math.Abs(count) < 2)
                return str;
            return pluralForm;
        }
    }
}
