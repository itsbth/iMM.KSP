using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using iMM.KSP.Lib.Base;

namespace iMM.KSP.Lib.Collection.ModFolder
{
    public class ModFolderModCollection : BaseModCollection
    {
        private readonly Lazy<List<Mod>> _lazyMods;
        private readonly string _path;

        public ModFolderModCollection(string path)
        {
            _path = path;
            _lazyMods = new Lazy<List<Mod>>(LoadMods);
        }

        public override IEnumerable<Mod> Mods
        {
            get { return _lazyMods.Value; }
        }

        public override IEnumerable<Plugin> Plugins
        {
            get { throw new NotImplementedException(); }
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

    internal class ModFolderMod : Mod
    {
        private readonly Lazy<List<Part>> _lazyParts;
        private readonly string _root;

        public ModFolderMod(string root, string id, string name, string author, string version)
            : base(id, name, author, version)
        {
            _lazyParts = new Lazy<List<Part>>(LoadParts);
            _root = root;
        }

        public override IEnumerable<Part> Parts
        {
            get { return _lazyParts.Value; }
        }

        private List<Part> LoadParts()
        {
            return
                new List<Part>(
                    Directory.GetDirectories(Path.Combine(_root, Id, "Parts"))
                             .Select(p => new {Path = p, Name = Path.GetFileName(p)})
                             .Select(p => new ModFolderPart(Path.Combine(_root, Id, "Parts"), p.Name, p.Name))
                             .ToList());
        }
    }

    public class ModFolderPart : Part
    {
        private readonly string _root;

        public ModFolderPart(string root, string id, string name) : base(id, name)
        {
            _root = root;
        }

        public override IEnumerable<string> Files
        {
            get { return Directory.GetFiles(Path.Combine(_root, Id)).Select(Path.GetFileName); }
        }

        public override Stream GetStream(string file)
        {
            return File.OpenRead(Path.Combine(_root, Id, file));
        }
    }
}