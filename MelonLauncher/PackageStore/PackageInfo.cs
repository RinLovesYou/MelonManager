using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelonLauncher.PackageStore
{
    public class PackageInfo
    {
        public string name;
        public string description;
        public string gameName;
        public string author;
        public string version;
        public string sourceName;
        public string downloadUrl;
        public string githubUrl;
        public string imageUrl;
        public string[] dependencies;
        public string FullName => $"{author}-{name}-{version}";
    }
}
