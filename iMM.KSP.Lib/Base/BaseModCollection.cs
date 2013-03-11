using System.Collections.Generic;

namespace iMM.KSP.Lib.Base
{
    public abstract class BaseModCollection
    {
        public abstract IEnumerable<Mod> Mods { get; }
        public abstract IEnumerable<Plugin> Plugins { get; }
    }
}