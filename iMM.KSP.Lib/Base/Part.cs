using System.Collections.Generic;
using System.IO;

namespace iMM.KSP.Lib.Base
{
    public abstract class Part : PartInfo
    {
        protected Part(string id, string name, Kind type) : base(id, name, type)
        {
        }

        public abstract IEnumerable<string> Files { get; }

        public abstract Stream GetStream(string file);
    }
}