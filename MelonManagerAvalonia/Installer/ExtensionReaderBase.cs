namespace MelonManagerAvalonia.Installer;

public abstract class ExtensionReaderBase
{
    public abstract string Extension { get; }

    public abstract string Read(string path);
}