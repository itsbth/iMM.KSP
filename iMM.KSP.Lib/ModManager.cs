using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Murmur;
using iMM.KSP.Lib.Base;
using iMM.KSP.Lib.Sources;

namespace iMM.KSP.Lib
{
    public class ModManager
    {
        private readonly GameInfo _info;
        private readonly BaseModSource _source;

        public ModManager(GameInfo info, BaseModSource source)
        {
            _info = info;
            _source = source;
            FilterFile = _ => _.Tag != ModFile.TypeTag.Other;
        }

        public Func<ModFile, bool> FilterFile { get; private set; }

        public IEnumerable<Mod> Mods
        {
            get { return _source.Mods; }
        }

        public bool IsModEnabled(Mod mod)
        {
            return _info.EnabledMods.Contains(mod.Id);
        }

        public void EnableMod(Mod mod)
        {
            if (IsModEnabled(mod)) return;
            foreach (ModFile file in mod.Files.Where(FilterFile))
            {
                string destination = Path.Combine(_info.Path, file.Path);
                Directory.CreateDirectory(Path.GetDirectoryName(destination));
                if (File.Exists(destination))
                {
                    if (IsSameFile(file.Source, destination))
                        continue;
                    Debug.WriteLine("File collision at {0} (from mod {1}).", file.Path, mod.Id);
                    continue;
                }
                File.Copy(file.Source, destination);
            }
            _info.EnabledMods.Add(mod.Id);
        }

        private bool IsSameFile(string source, string destination)
        {
            using (Stream ss = File.OpenRead(source), ds = File.OpenRead(destination))
            {
                var murmur = MurmurHash.Create128(managed: false);
                return murmur.ComputeHash(ss).SequenceEqual(murmur.ComputeHash(ds));
            }
        }

        public void DisableMod(Mod mod)
        {
            foreach (ModFile file in mod.Files)
            {
                string path = Path.Combine(_info.Path, file.Path);
                if (!File.Exists(path)) continue;
                string directory = Path.GetDirectoryName(path);
                if (File.Exists(path))
                    File.Delete(path);
                Debug.Assert(directory != null, "directory != null");
                if (!Directory.EnumerateFileSystemEntries(directory).Any())
                    Directory.Delete(directory);
            }
            _info.EnabledMods.Remove(mod.Id);
        }

        public void ToggleMod(Mod mod)
        {
            if (IsModEnabled(mod))
                DisableMod(mod);
            else
                EnableMod(mod);
        }
    }
}