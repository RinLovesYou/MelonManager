using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

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
