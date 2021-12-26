using System.IO;
using System.Linq;
using MelonManagerAvalonia.Installer.ExtensionReaders;

namespace MelonManagerAvalonia.Installer;

public static class PathFinder
{
    private static ExtensionReaderBase[] readers = new ExtensionReaderBase[]
    {
        new Shortcut(),
        new Url()
    };

    public static string FindPathOfExe(string path)
    {
        var ext = Path.GetExtension(path);
        if (ext == ".exe")
            return path;

        var reader = readers.FirstOrDefault(x => x.Extension == ext);
        if (reader == null)
            return null;

        return reader.Read(path);
    }
}