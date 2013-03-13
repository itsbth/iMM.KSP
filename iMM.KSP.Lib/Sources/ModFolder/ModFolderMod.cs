using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using iMM.KSP.Lib.Base;

namespace iMM.KSP.Lib.Sources.ModFolder
{
    internal class ModFolderMod : Mod
    {
        private readonly Lazy<List<string>> _lazyParts;
        private readonly string _root;

        public ModFolderMod(string root, string id, string name, string author, string version)
            : base(id, name, author, version)
        {
            _lazyParts = new Lazy<List<string>>(LoadFiles);
            _root = root;
        }

        private List<string> LoadFiles()
        {
            return Directory.EnumerateFiles(Path.Combine(_root, Id)).ToList();
        }

        public override IEnumerable<string> Files
        {
            get { return _lazyParts.Value; }
        }
    }
}