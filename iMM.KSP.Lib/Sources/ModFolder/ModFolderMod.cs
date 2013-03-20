using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using iMM.KSP.Lib.Base;

namespace iMM.KSP.Lib.Sources.ModFolder
{
    internal class ModFolderMod : Mod
    {
        private readonly Lazy<List<ModFile>> _lazyParts;
        private readonly string _root;

        public ModFolderMod(string root, string id, string name, string author, string version)
            : base(id, name, author, version)
        {
            _lazyParts = new Lazy<List<ModFile>>(LoadFiles);
            _root = Path.Combine(root, Id);
        }

        private List<ModFile> LoadFiles()
        {
            return Directory.EnumerateFiles(_root, "*", SearchOption.AllDirectories).Select(CreateFile).ToList();
        }

        private ModFile CreateFile(string fp)
        {
            string path = fp.Substring(_root.Length + 1);
            string root = path.Split(Path.DirectorySeparatorChar).First();
            ModFile.TypeTag tag;
            switch (root.ToUpperInvariant())
            {
                case "PARTS":
                    tag = ModFile.TypeTag.Part;
                    break;
                case "INTERNALS":
                    tag = ModFile.TypeTag.Interior;
                    break;
                case "RESOURCES":
                    tag = ModFile.TypeTag.Resource;
                    break;
                case "PLUGINS":
                    tag = ModFile.TypeTag.Plugin;
                    break;
                case "PLUGINDATA":
                    tag = ModFile.TypeTag.PluginData;
                    break;
                default:
                    tag = ModFile.TypeTag.Other;
                    break;
            }
            var file = new ModFile(fp, path, tag);
            return file;
        }

        public override IEnumerable<ModFile> Files
        {
            get { return _lazyParts.Value; }
        }
    }
}