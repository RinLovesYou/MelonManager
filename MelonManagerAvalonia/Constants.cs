using System.Reflection;

namespace MelonManagerAvalonia;

public class Constants
{
    internal const string MelonLoaderGitApi = "https://api.github.com/repos/LavaGang/MelonLoader/releases";
    internal const string MelonLoaderReleases = "https://github.com/LavaGang/MelonLoader/releases";
    internal const string SelfReleasesGitApi = "https://api.github.com/repos/SlidyDev/MelonManager/releases";
    internal const string SelfReleases = "https://github.com/SlidyDev/MelonManager/releases";
    internal const string Discord = "https://discord.gg/2Wn3N2P";
    internal const string Twitter = "https://twitter.com/lava_gang";
    internal const string MLGitHub = "https://github.com/LavaGang/MelonLoader";
    internal const string MMGitHub = "https://github.com/SlidyDev/MelonManager";
    internal const string Wiki = "https://melonwiki.xyz";
    internal const string LavaGang = "https://github.com/LavaGang";

    internal const string SlidyDevGit = "https://github.com/SlidyDev";
    internal const string SamboyGit = "https://github.com/SamboyCoding";
    internal const string RinGit = "https://github.com/RinLovesYou";
    
    internal static string Version => Assembly.GetExecutingAssembly().GetName().Version.ToString().Remove(3);
}