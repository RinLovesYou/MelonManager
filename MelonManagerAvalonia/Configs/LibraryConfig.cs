using System.Collections.Generic;
using MelonManagerAvalonia.Games;
using MelonManagerAvalonia.Melons;

namespace MelonManagerAvalonia.Configs;

public class LibraryConfig
{
    public List<LibGameCache> libGames = new List<LibGameCache>();

    public class LibGameCache
    {
        public LibGameCache() { }
        public LibGameCache(LibraryGame game)
        {
            path = game.info.path;
            mods = game.mods;
            plugins = game.plugins;
        }

        public string path;
        public List<Melon> mods = new List<Melon>();
        public List<Melon> plugins = new List<Melon>();
    }
}