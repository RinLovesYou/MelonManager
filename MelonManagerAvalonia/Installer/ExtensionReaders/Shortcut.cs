namespace MelonManagerAvalonia.Installer.ExtensionReaders;

public class Shortcut : ExtensionReaderBase
{
    public override string Extension => ".lnk";

    private string ParseLNK(string shortcut_path)
    {
        //TODO Windows users pls: return ((IWshRuntimeLibrary.IWshShortcut)new IWshRuntimeLibrary.WshShell().CreateShortcut(shortcut_path)).TargetPath;
        return null;
    }

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