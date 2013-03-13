using System.Collections.Generic;
using iMM.KSP.Lib.Base;

namespace iMM.KSP.Lib.Sources
{
    public abstract class BaseModSource
    {
        public abstract IEnumerable<Mod> Mods { get; }
    }
}