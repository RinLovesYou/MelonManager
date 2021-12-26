using System;
using System.IO;
using MelonManagerAvalonia.Managers;

namespace MelonManagerAvalonia.Games;

public class GameInfo
    {
        public readonly string path;
        public readonly string dir;
        public readonly string name;
        public readonly string executableName;
        public readonly string author;
        public bool x86;
        public readonly bool hasOfficialInfo;

        private GameInfo(string gameExePath)
        {
            path = gameExePath;
            dir = Path.GetDirectoryName(path);
            executableName = Path.GetFileNameWithoutExtension(path);
            name = executableName;
            author = string.Empty;
            {
                byte[] array = File.ReadAllBytes(path);
                x86 = array == null || array.Length == 0 || BitConverter.ToUInt16(array, BitConverter.ToInt32(array, 60) + 4) != 34404;
            }
            string appInfoPath = Path.Combine(dir, name + @"_Data\app.info");
            bool hasAppInfo = File.Exists(appInfoPath);
            if (hasAppInfo)
            {
                string appInfo = File.ReadAllText(appInfoPath);
                if (appInfo.Contains('\n'))
                {
                    var info = appInfo.Split('\n');
                    bool hasAuthor = info[0].Length > 0;
                    bool hasName = info[1].Length > 0;
                    if (hasAuthor)
                        author = info[0];
                    if (hasName)
                        name = info[1];
                    if (hasName && hasAuthor)
                        hasOfficialInfo = true;
                }
            }
        }

        public static GameInfo Create(string gameExePath)
        {
            gameExePath = gameExePath.Replace("\r", string.Empty);
            if (!File.Exists(gameExePath))
            {
                Logger.Log($"Failed to create Info for '{gameExePath}' because it doesn't exist!", Logger.Level.Error);
                return null;
            }
            var dir = Path.GetDirectoryName(gameExePath);
            if (!File.Exists(Path.Combine(dir, "UnityPlayer.dll")) || !Directory.Exists(Path.Combine(dir, gameExePath.Remove(gameExePath.Length - 4) + "_Data")))
            {
                Logger.Log($"Failed to create Info for '{gameExePath}' because it isn't a unity game!", Logger.Level.Error);
                return null;
            }

            return new GameInfo(gameExePath);
        }

        public override string ToString()
        {
            return $"{author} - {name}";
        }

        public override bool Equals(object obj)
        {
            if (obj is GameInfo inf)
                return path == inf.path;
            return false;
        }

        public override int GetHashCode()
        {
            return path.GetHashCode();
        }

        public static bool operator ==(GameInfo inf1, GameInfo inf2)
        {
            if ((object)inf1 == null || (object)inf2 == null)
                return (object)inf1 == (object)inf2; // Ik this looks stupid but its actually ok lmao

            return inf1.path == inf2.path;
        }

        public static bool operator !=(GameInfo inf1, GameInfo inf2)
        {
            if ((object)inf1 == null || (object)inf2 == null)
                return (object)inf1 != (object)inf2; // Ik this looks stupid but its actually ok lmao

            return inf1.path != inf2.path;
        }
    }