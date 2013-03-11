using System.Collections.Generic;

namespace iMM.KSP.Lib.Base
{
    public abstract class ModCollection
    {
        public abstract IEnumerable<Mod> Mods { get; }
        public abstract IEnumerable<Plugin> Plugins { get; }
    }
}