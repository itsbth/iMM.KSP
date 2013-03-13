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
        private readonly Lazy<List<Plugin>> _lazyPlugins;
        private readonly string _path;

        public ModFolderSource(string path)
        {
            _path = path;
            _lazyMods = new Lazy<List<Mod>>(LoadMods);
            _lazyPlugins = new Lazy<List<Plugin>>(LoadPlugins);
        }

        private List<Plugin> LoadPlugins()
        {
            var plugins = new List<Plugin>();
            foreach (var mod in Directory.GetDirectories(_path))
            {
                if (!Directory.Exists(Path.Combine(mod, "Plugins"))) continue;
                var binaries = Directory.GetFiles(Path.Combine(mod, "Plugins"));
                if (binaries.Length == 0)
                    continue;
                if (binaries.Length > 1)
                    throw new Exception("Oh, bugger.");
                if (Directory.Exists(Path.Combine(mod, "PluginData")))
                {}
            }
            return plugins;
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