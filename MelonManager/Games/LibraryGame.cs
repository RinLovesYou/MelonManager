using MelonLoader.Interfaces;
using MelonLoader.Managers;
using MelonManager.Configs;
using MelonManager.Forms;
using MelonManager.Managers;
using MelonManager.MelonLoader;
using MelonManager.Melons;
using MelonManager.Tasks;
using MelonManager.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Security.Cryptography;

namespace MelonManager.Games
{
    public class LibraryGame
    {
        #region Static
        // string is the file/folder name and bool is folder if true
        public static readonly Tuple<string, bool>[] mlRootPaths =
        {
            new Tuple<string, bool>("MelonLoader", true),
            new Tuple<string, bool>("version.dll", false),
            new Tuple<string, bool>("winhttp.dll", false),
            new Tuple<string, bool>("winmm.dll", false),
            new Tuple<string, bool>("NOTICE.txt", false),
        };

        private static ThreadSafeList<LibraryGame> libGames = new ThreadSafeList<LibraryGame>();
        public static IReadOnlyList<LibraryGame> LibraryGames => libGames;
        public static FormsSafeEvent<LibraryGame> onGameAdded = new FormsSafeEvent<LibraryGame>();
        public static FormsSafeEvent<LibraryGame> onGameRemoved = new FormsSafeEvent<LibraryGame>();
        public static readonly Config<LibraryConfig> config = new Config<LibraryConfig>("Library");

        static LibraryGame()
        {
            Logger.Log("Loading library...");

            foreach (var g in config.config.libGames)
            {
                var l = AddGame(GameInfo.Create(g.path));
                if (l == null)
                    continue;
                l.mods = g.mods;
                l.plugins = g.plugins;
                l.RefreshMelons();
            }
            Logger.Log("Library loaded!");
        }

        public static void SaveLibrary()
        {
            Logger.Log("Saving library...");
            config.config.libGames = new List<LibraryConfig.LibGameCache>(libGames.Select(x => new LibraryConfig.LibGameCache(x)));
            config.Save();
        }

        public static LibraryGame GetLibGame(string path) =>
            libGames.FirstOrDefault(x => x.info.path == path);

        public static LibraryGame AddGame(GameInfo game, bool verifyML = true)
        {
            if (game == null)
                return null;

            var result = libGames.FirstOrDefault(x => x.info == game);
            if (result != null)
                return result;

            var ml = MLInfo.GetInfo(Path.GetDirectoryName(game.path));
            if (verifyML && ml == null)
                return null;

            var libG = new LibraryGame(game, ml);
            libGames.Add(libG);
            onGameAdded.Invoke(libG);
            Logger.Log($"Added '{game.path}' to the library.");
            return libG;
        }
        #endregion

        public GameInfo info;
        public MLInfo ml;
        public FormsSafeEvent onMelonsRefreshed = new FormsSafeEvent();
        public FormsSafeEvent onMlVersionChange = new FormsSafeEvent();
        public FormsSafeEvent onRemoved = new FormsSafeEvent();
        public FormsSafeEvent onInstallingStateChanged = new FormsSafeEvent();
        public List<Melon> mods = new List<Melon>();
        public List<Melon> plugins = new List<Melon>();
        public List<string> invalidMods = new List<string>();
        public List<string> invalidPlugins = new List<string>();
        private bool removed;
        private volatile bool isVerifying;
        private bool installing;
        public bool Installing
        {
            get => installing;
            private set
            {
                if (value == installing)
                    return;

                installing = value;
                onInstallingStateChanged.Invoke();
            }
        }

        private LibraryGame(GameInfo info, MLInfo ml)
        {
            this.info = info;
            this.ml = ml;
        }

        public void RemoveInvalidMelons(bool plugins)
        {
            var invalidsList = plugins ? invalidPlugins : invalidMods;
            foreach (var i in invalidsList)
            {
                if (!File.Exists(i))
                    return;

                File.Delete(i);
            }
            invalidsList.Clear();

            RefreshMelons(plugins);
        }

        public void RefreshMelons()
        {
            RefreshMelons(false);
            RefreshMelons(true);
        }

        public void RefreshMelons(bool plugins)
        {
            Logger.Log($"Refreshing {(plugins ? "plugins" : "mods")} from '{info}'");
            var melonsPath = Path.Combine(info.dir, plugins ? "Plugins" : "Mods");
            if (!Directory.Exists(melonsPath))
            {
                Directory.CreateDirectory(melonsPath); // Cuz im nice :)
                return;
            }

            var melonPaths = Directory.GetFiles(melonsPath, "*.dll");
            foreach (var m in melonPaths)
            {
                VerifyMelon(m, plugins);
            }

            var list = plugins ? this.plugins : mods;
            for (int a = 0; a < list.Count; a++)
            {
                var m = list[a];
                if (!melonPaths.Contains(m.path))
                {
                    list.RemoveAt(a);
                    a--;
                }
            }
            onMelonsRefreshed.Invoke();
            Logger.Log($"All {(plugins ? "plugins" : "mods")} from '{info}' refreshed!");
        }

        private Melon VerifyMelon(string melonPath, bool isPlugin, bool newMelon = false)
        {
            melonPath = Path.GetFullPath(melonPath);
            var str = File.OpenRead(melonPath);
            var hash = SimpleSHA512.Compute(str);

            var list = isPlugin ? plugins : mods;
            var existingMelon = list.FirstOrDefault(x => x.hash == hash);
            if (existingMelon != null)
            {
                str.Dispose();

                if (existingMelon.path != melonPath)
                    existingMelon.path = melonPath;

                if (newMelon)
                    CustomMessageBox.Error($"The {(isPlugin ? "plugin" : "mod")} '{existingMelon.name}' is already installed!");
                return existingMelon;
            }

            str.Seek(0, SeekOrigin.Begin);
            var melon = Melon.Open(melonPath, str, newMelon);
            str.Dispose();

            var invalidsList = isPlugin ? invalidPlugins : invalidMods;
            if (melon == null)
            {
                if (!newMelon && !invalidsList.Contains(melonPath))
                    invalidsList.Add(melonPath);

                return null;
            }
            if (!melon.CheckCompatibility(info))
            {
                if (invalidsList.Contains(melonPath))
                    invalidsList.Add(melonPath);

                Logger.Log($"Melon '{melon.name}' is incompatible with {info}!", Logger.Level.Warning);
                if (newMelon)
                    CustomMessageBox.Error($"The {(isPlugin ? "plugin" : "mod")} '{melon.name}' is incompatible with {info.name}.");
                else if (!invalidsList.Contains(melonPath))
                    invalidsList.Add(melonPath);

                return null;
            }
            if (melon.isPlugin != isPlugin)
            {

                Logger.Log($"{(isPlugin ? "Mod" : "Plugin")} '{melon.name}' cannot be added to the {(isPlugin ? "plugins" : "mods")} list of '{info}'", Logger.Level.Warning);
                if (newMelon)
                    CustomMessageBox.Error($"The melon you're trying to add is a {(isPlugin ? "mod" : "plugin")} and cannot be added to the {(isPlugin ? "plugins" : "mods")} list.");
                else if (!invalidsList.Contains(melonPath))
                    invalidsList.Add(melonPath);

                return null;
            }

            if (invalidsList.Contains(melonPath))
                invalidsList.Remove(melonPath);

            list.Add(melon);
            return melon;
        }

        public void AddMelon(string path, bool isPlugin)
        {
            var melon = VerifyMelon(path, isPlugin, true);
            if (melon == null)
                return;

            var melonsPath = Path.Combine(info.dir, isPlugin ? "Plugins" : "Mods");
            var destination = Path.Combine(melonsPath, Path.GetFileName(path));
            if (Path.GetFullPath(path) != destination)
            {
                if (File.Exists(destination))
                    destination = destination.Remove(destination.Length - 4) + " " + new Random().Next(int.MaxValue).ToString() + Path.GetExtension(destination);

                File.Copy(path, destination);
                melon.path = destination;
            }
        }

        public void Remove()
        {
            if (removed || Installing)
                return;
            removed = true;
            libGames.Remove(this);
            onRemoved.Invoke();
            onGameRemoved.Invoke(this);
            Logger.Log($"Removed '{info.path}' from the library.");
        }

        public void VerifyVersion(bool removeIfNone = true)
        {
            if (isVerifying)
                return;
            isVerifying = true;
            var m = MLInfo.GetInfo(info.dir);
            isVerifying = false;
            if (removeIfNone && m == null)
            {
                Logger.Log($"Removing '{info}' from the library, because ML wasn't found", Logger.Level.Warning);
                Remove();
                return;
            }
            if (((m == null || ml == null) && m == ml) || (m != null && ml != null && UtilsBox.CompareVersions(m.version, ml.version) == 0))
                return;

            ml = m;
            onMlVersionChange.Invoke();
        }

        public void InstallML(string version)
        {
            Installing = true;
            var t = new Task($"Installing MelonLoader {version} for '{info.name}'", (task) => InstallMLTaskDownload(task, version));
            t.onFinishedCallback.Subscribe(OnInstallFinished);
            t.Init();
        }

        public void UninstallML(bool removeFromLib)
        {
            foreach (var a in mlRootPaths)
            {
                var path = Path.Combine(info.dir, a.Item1);
                if (a.Item2)
                {
                    if (Directory.Exists(path))
                        Directory.Delete(path, true);
                }
                else
                {
                    if (File.Exists(path))
                        File.Delete(path);
                }
            }

            if (removeFromLib)
                Remove();
        }

        private void OnInstallFinished(Task task)
        {
            if (task.Failed)
            {
                Installing = false;
                Remove();
                return;
            }
            VerifyVersion();
            Installing = false;
        }

        private void InstallMLTaskDownload(Task task, string version)
        {
            Task.Status = "Checking version";
            if (string.IsNullOrEmpty(version))
            {
                task.FailTask("Version is null or empty.");
                return;
            }

            if (version[0] != 'v')
                version = 'v' + version;

            if (!GitHub.releasesTbl.Any(x => x.Version == version))
            {
                task.FailTask("Invalid version selected.");
                return;
            }

            var legacy = version.StartsWith("v0.1") || version.StartsWith("v0.2");

            if (legacy && info.x86)
            {
                task.FailTask("The selected MelonLoader version is incompatible with the game's arch.");
                return;
            }

            Task.Status = "Preparing for download";
            string uriString = string.Concat(new string[]
            {
                Constants.MelonLoaderReleases,
                "/download/",
                version,
                "/MelonLoader.",
                info.x86 ? "x86" : "x64",
                ".zip"
            });

            var mlTempPath = TempPath.CreateTempFile();
            task.onFinishedCallback.Subscribe((task) => File.Delete(mlTempPath));

            var wc = new WebClient();
            wc.DownloadProgressChanged += (sender, e) => Task.ProgressPercentage = e.ProgressPercentage / 2; // Heck yea, good old lambda expressions, more useless nested classes
            wc.DownloadFileCompleted += (sender, e) =>
            {
                wc.Dispose();
                if (e.Error != null)
                {
                    task.FailTask("Failed to download MelonLoader " + version + ".", e.Error);
                    return;
                }
                else if (e.Cancelled)
                {
                    task.FailTask("The download of MelonLoader " + version + "has been cancelled.");
                    return;
                }

                InstallMLTaskVerify(task, version, mlTempPath);
            };

            Task.Status = "Downloading...";
            try
            {
                wc.DownloadFileAsync(new Uri(uriString), mlTempPath);
            }
            catch (Exception ex)
            {
                wc.Dispose();
                task.FailTask("Failed to download MelonLoader " + version + ".", ex);
                return;
            }
        }

        private void InstallMLTaskVerify(Task task, string version, string dwnPath)
        {
            Task.Status = "Verifying downloaded package";
            string address = string.Concat(new string[]
            {
                Constants.MelonLoaderReleases,
                "/download/",
                version,
                "/MelonLoader.",
                info.x86 ? "x86" : "x64",
                ".sha512"
            });

            var wc = new WebClient();

            wc.DownloadProgressChanged += (sender, e) => Task.ProgressPercentage = 50 + e.ProgressPercentage / 20;
            wc.DownloadStringCompleted += (sender, e) =>
            {
                wc.Dispose();

                if (e.Error != null)
                {
                    task.FailTask("Failed to download the SHA512 Hash of MelonLoader " + version + ".", e.Error);
                    return;
                }
                else if (e.Cancelled)
                {
                    task.FailTask("The download of the SHA512 Hash of MelonLoader " + version + "has been cancelled.");
                    return;
                }

                var hash = e.Result;
                byte[] array = new SHA512Managed().ComputeHash(File.ReadAllBytes(dwnPath));
                Task.ProgressPercentage = 55;
                if (array == null || array.Length == 0)
                {
                    task.FailTask("Failed to compute a SHA512 Hash of the downloaded package.");
                    return;
                }

                string text = BitConverter.ToString(array).Replace("-", string.Empty);
                if (string.IsNullOrEmpty(text))
                {
                    task.FailTask("Failed to convert the computed SHA512 hash to string.");
                    return;
                }

                if (!text.Equals(hash))
                {
                    task.FailTask("The downloaded package is either corrupted or has been edited, the SHA512 Hash doesn't match the original one!");
                    return;
                }

                InstallMLTaskExtract(task, dwnPath);
            };

            wc.DownloadStringAsync(new Uri(address));
        }

        private void InstallMLTaskExtract(Task task, string dwnPath)
        {
            if (ml != null)
            {
                Task.Status = "Uninstalling previous version";
                UninstallML(false);
            }

            Task.Status = "Extracting package";
            var str = File.Open(dwnPath, FileMode.Open, FileAccess.Read, FileShare.Read);
            var archive = new ZipArchive(str);
            var entries = archive.Entries.ToArray();
            long finalSize = 0;
            foreach (var ent in entries)
                finalSize += ent.Length;
            var len = entries.Length;
            long finishedBytes = 0;
            for (int a = 0; a < len; a++)
            {
                var file = entries[a];
                string completeFileName = Path.Combine(info.dir, file.FullName);
                Directory.CreateDirectory(Path.GetDirectoryName(completeFileName));
                if (string.IsNullOrEmpty(file.Name))
                    continue;
                file.ExtractToFile(completeFileName, true);
                finishedBytes += file.Length;
                Task.ProgressPercentage = (int)(55d + finishedBytes / (double)finalSize * 45d);
            }
            archive.Dispose();
            str.Dispose();

            task.FinishTask();
        }
    }
}
