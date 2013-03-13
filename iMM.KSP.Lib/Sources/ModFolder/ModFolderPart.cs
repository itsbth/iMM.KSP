using System.Collections.Generic;
using System.IO;
using System.Linq;
using iMM.KSP.Lib.Base;

namespace iMM.KSP.Lib.Sources.ModFolder
{
    public class ModFolderPart : Part
    {
        private readonly string _root;
        private List<string> _files;

        public ModFolderPart(string root, string id, string name, Kind type) : base(id, name, type)
        {
            _root = root;
            string partFolder = Path.Combine(_root, Id);
            _files = Directory.EnumerateFiles(partFolder).Select(fn => fn.Substring(partFolder.Length + 1)).ToList();
        }

        public override IEnumerable<string> Files
        {
            get { return _files; }
        }

        public override Stream GetStream(string file)
        {
            return File.OpenRead(Path.Combine(_root, Id, file));
        }
    }
}