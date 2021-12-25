namespace MelonManager.Installer.ExtensionReaders
{
    public class Shortcut : ExtensionReaderBase
    {
        public override string Extension => ".lnk";

        private string ParseLNK(string shortcut_path)
            => ((IWshRuntimeLibrary.IWshShortcut)new IWshRuntimeLibrary.WshShell().CreateShortcut(shortcut_path)).TargetPath;

        public override string Read(string path)
        {
            try
            {
                return ParseLNK(path);
            }
            catch { }

            return null;
        }
    }
}