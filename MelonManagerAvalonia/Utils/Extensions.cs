using System;

namespace MelonManagerAvalonia.Utils;

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