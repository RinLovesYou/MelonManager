using System.Linq;
using MelonManager.Forms;
using Mono.Cecil;

namespace MelonManager.Melons
{
    public class Melon
    {
        public readonly string name;
        public readonly string author;
        public readonly string version;
        public readonly string downloadLink;

        private Melon(string name, string author, string version, string downloadLink)
        {
            this.name = name;
            this.author = author;
            this.version = version;
            this.downloadLink = downloadLink;
        }

        public static bool Open(string path, LibraryGame game, out Melon melon)
        {
            melon = null;
            ModuleDefinition module;
            try
            {
                module = ModuleDefinition.ReadModule(path);
            }
            catch
            {
                return false;
            }
            var attrs = module.Assembly.CustomAttributes;
            var info = attrs.FirstOrDefault(x =>
            {
                string name = x.AttributeType.FullName;
                return name == "MelonLoader.MelonInfoAttribute" || name == "MelonLoader.MelonPluginInfoAttribute" || name == "MelonLoader.MelonModInfoAttribute";
            });
            if (info == null)
            {
                module.Dispose();
                return false;
            }
            var gameAttrs = attrs.Where(x => 
            {
                string name = x.AttributeType.FullName;
                return name == "MelonLoader.MelonGameAttribute" || name == "MelonLoader.MelonPluginGameAttribute" || name == "MelonLoader.MelonModGameAttribute";
            });
            bool compatible = false;
            foreach (var attr in gameAttrs)
            {
                var args = attr.ConstructorArguments;
                var dev = args[0].Value;
                var name = args[1].Value;
                if (!game.info.hasOfficialInfo || ((dev == null || (string)dev == game.info.author) && (dev == null || (string)dev == game.info.author)))
                {
                    compatible = true;
                    break;
                }
            }
            if (!compatible)
            {
                module.Dispose();
                return false;
            }

            var infoArgs = info.ConstructorArguments;
            string melonName = (string)infoArgs[1].Value;
            string melonAuthor = (string)infoArgs[3].Value;
            string melonVersion = (string)infoArgs[2].Value;
            string melonDownloadLink = (string)infoArgs[4].Value;

            melon = new Melon(melonName, melonAuthor, melonVersion, melonDownloadLink);
            module.Dispose();
            return true; 
        }
    }
}
