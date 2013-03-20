using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using iMM.KSP.Lib.Base;

namespace iMM.KSP.Lib.Sources.ModFolder
{
    public class ModFolderSource : BaseModSource
    {
        private readonly Lazy<List<Mod>> _lazyMods;
        private readonly string _path;

        public ModFolderSource(string path)
        {
            _path = path;
            _lazyMods = new Lazy<List<Mod>>(LoadMods);
        }

        public override IEnumerable<Mod> Mods
        {
            get { return _lazyMods.Value; }
        }

        private List<Mod> LoadMods()
        {
            return
                new List<Mod>(
                    Directory.GetDirectories(_path)
                             .Select(p => new {Path = p, Name = Path.GetFileName(p)})
                             .Select(p => new ModFolderMod(_path, p.Name, p.Name, "n/a", "0.0")));
        }
    }
}