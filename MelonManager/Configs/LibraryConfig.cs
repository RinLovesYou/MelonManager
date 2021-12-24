using MelonManager.Games;
using MelonManager.Melons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelonManager.Configs
{
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
}
