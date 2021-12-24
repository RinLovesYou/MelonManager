using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MelonManager.Forms;
using MelonManager.Games;
using MelonManager.Managers;
using MelonManager.Utils;
using Mono.Cecil;

namespace MelonManager.Melons
{
    public class Melon
    {
        public string path;
        public string name;
        public string author;
        public string version;
        public string downloadLink;
        public bool isPlugin;
        public CompatibilityLayer[] compatibilityLayers;
        public string hash;
        public bool IsUniversal => compatibilityLayers.Length == 0;

        public Melon() { }
        private Melon(string path, string name, string author, string version, string downloadLink, bool isPlugin, CompatibilityLayer[] compatibilityLayers, string hash)
        {
            this.path = path;
            this.name = name;
            this.author = author;
            this.version = version;
            this.downloadLink = downloadLink;
            this.isPlugin = isPlugin;
            this.compatibilityLayers = compatibilityLayers;
            this.hash = hash;
        }

        public bool CheckCompatibility(GameInfo info) =>
            compatibilityLayers.Length == 0 || compatibilityLayers.Any(x => x.name == info.name && x.author == info.author);

        public static Melon Open(string path, Stream stream, bool showFailReason)
        {
            ModuleDefinition module;
            try
            {
                module = ModuleDefinition.ReadModule(stream);
            }
            catch (BadImageFormatException)
            {
                Logger.Log($"Failed to open Melon '{path}': The selected DLL is not a valid .net library", Logger.Level.Error);
                if (showFailReason)
                    CustomMessageBox.Error($"Failed to open '{path}': The selected DLL is not a valid .net library!");
                return null;
            }
            catch (Exception ex)
            {
                Logger.Log($"Failed to open Melon '{path}':\n{ex}", Logger.Level.Error);
                if (showFailReason)
                    CustomMessageBox.Error($"Failed to open '{path}' as a Melon:\n\n{ex}");
                return null;
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
                Logger.Log($"Failed to open Melon '{path}': No MelonInfo attribute", Logger.Level.Error);
                if (showFailReason)
                    CustomMessageBox.Error($"Failed to open '{path}' as a Melon: This DLL is not a valid Melon!");
                return null;
            }

            var infoArgs = info.ConstructorArguments;
            var melonType = (TypeDefinition)infoArgs[0].Value;
            var isPlugin = TypeInheritsFrom(melonType, "MelonLoader.MelonPlugin");
                
            string melonName = (string)infoArgs[1].Value;
            string melonAuthor = (string)infoArgs[3].Value;
            string melonVersion = (string)infoArgs[2].Value;
            string melonDownloadLink = (string)infoArgs[4].Value;

            var clAttrs = attrs.Where(x => 
            {
                string name = x.AttributeType.FullName;
                return name == "MelonLoader.MelonGameAttribute" || name == "MelonLoader.MelonPluginGameAttribute" || name == "MelonLoader.MelonModGameAttribute";
            });

            var cls = new List<CompatibilityLayer>();
            foreach (var attr in clAttrs)
            {
                var args = attr.ConstructorArguments;
                var dev = (string)args[0].Value;
                var name = (string)args[1].Value;
                if (name == null && dev == null)
                    continue;

                cls.Add(new CompatibilityLayer(name, dev));
            }

            module.Dispose();

            var str = File.OpenRead(path);
            var hash = SimpleSHA512.Compute(str);
            str.Dispose();
            return new Melon(path, melonName, melonAuthor, melonVersion, melonDownloadLink, isPlugin, cls.ToArray(), hash);
        }

        private static bool TypeInheritsFrom(TypeDefinition type, string typeFullName)
        {
            if (type == null)
                return false;
            var b = type.BaseType;
            if (b.FullName == "System.Object")
                return false;

            Logger.Log(b.FullName);
            if (b.FullName == typeFullName)
                return true;

            TypeDefinition bDef = null;
            try
            {
                bDef = b.Resolve();
            }
            catch { }
            return TypeInheritsFrom(bDef, typeFullName);
        }
    }
}
