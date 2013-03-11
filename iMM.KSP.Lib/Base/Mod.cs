using System.Collections.Generic;

namespace iMM.KSP.Lib.Base
{
    public abstract class Mod : ModInfo
    {
        protected Mod(string id, string name, string author, string version) : base(id, name, author, version)
        {
        }

        public abstract IEnumerable<Part> Parts { get; }
    }
}