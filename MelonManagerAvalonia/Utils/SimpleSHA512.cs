using System;
using System.IO;
using System.Security.Cryptography;

namespace MelonManagerAvalonia.Utils;

public static class SimpleSHA512
{
    public static string Compute(Stream stream)
    {
        var array = SHA512.Create().ComputeHash(stream);
        var hash = BitConverter.ToString(array).Replace("-", string.Empty);
        return hash;
    }
}