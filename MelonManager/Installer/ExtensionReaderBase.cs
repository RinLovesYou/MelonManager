using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelonManager.Installer
{
    public abstract class ExtensionReaderBase
    {
        public abstract string Extension { get; }

        public abstract string Read(string path);
    }
}
