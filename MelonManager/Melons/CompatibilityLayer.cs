using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelonManager.Melons
{
    public struct CompatibilityLayer
    {
        public string name;
        public string author;

        public CompatibilityLayer(string name, string author)
        {
            this.name = name;
            this.author = author;
        }
    }
}
