﻿using System;
using System.Linq;
using MelonManager.Forms;
using MelonManager.Managers;
using Mono.Cecil;

namespace MelonManager.Melons
{
    public class Melon
    {
        public readonly string path;
        public readonly string name;
        public readonly string author;
        public readonly string version;
        public readonly string downloadLink;

        private Melon(string path, string name, string author, string version, string downloadLink)
        {
            this.path = path;
            this.name = name;
            this.author = author;
            this.version = version;
            this.downloadLink = downloadLink;
        }

        public static bool Open(string path, LibraryGame game, bool showFailReason, out Melon melon)
        {
            melon = null;
            ModuleDefinition module;
            try
            {
                module = ModuleDefinition.ReadModule(path);
            }
            catch (BadImageFormatException)
            {
                Logger.Log($"Failed to open Melon '{path}' for game '{game.info}': The selected DLL is not a valid .net library", Logger.Level.Error);
                if (showFailReason)
                    CustomMessageBox.Error($"Failed to open '{path}' as a Melon: The selected DLL is not a valid .net library!");
                return false;
            }
            catch (Exception ex)
            {
                Logger.Log($"Failed to open Melon '{path}' for game '{game.info}':\n{ex}", Logger.Level.Error);
                if (showFailReason)
                    CustomMessageBox.Error($"Failed to open '{path}' as a Melon:\n\n{ex}");
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
                Logger.Log($"Failed to open Melon '{path}' for game '{game.info}': No MelonInfo attribute", Logger.Level.Error);
                if (showFailReason)
                    CustomMessageBox.Error($"Failed to open '{path}' as a Melon: This DLL is not a valid Melon!");
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
                Logger.Log($"Failed to open Melon '{path}' for game '{game.info}' because it's incompatible!", Logger.Level.Error);
                if (showFailReason)
                    CustomMessageBox.Error($"Failed to open '{path}' as a Melon:\nThe selected Melon is incompatible with {game.info.name}!");
                return false;
            }

            var infoArgs = info.ConstructorArguments;
            string melonName = (string)infoArgs[1].Value;
            string melonAuthor = (string)infoArgs[3].Value;
            string melonVersion = (string)infoArgs[2].Value;
            string melonDownloadLink = (string)infoArgs[4].Value;

            melon = new Melon(path, melonName, melonAuthor, melonVersion, melonDownloadLink);
            module.Dispose();
            return true; 
        }
    }
}
