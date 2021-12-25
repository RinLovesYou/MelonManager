using System;
using System.IO;
using System.Security.Cryptography;

namespace MelonManager.Utils
{
    public static class SimpleSHA512
    {
        public static string Compute(Stream stream)
        {
            var array = new SHA512Managed().ComputeHash(stream);
            var hash = BitConverter.ToString(array).Replace("-", string.Empty);
            return hash;
        }
    }
}
