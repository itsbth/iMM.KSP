using System.Collections.Generic;
using iMM.KSP.Lib.Base;
using iMM.KSP.Lib.Enabler;

namespace iMM.KSP.Lib
{
    public class ModManager
    {
        private readonly BaseModCollection _collection;
        private readonly GameInfo _info;
        private readonly BasePartEnabler _enabler;

        public ModManager(GameInfo info, BaseModCollection collection)
        {
            _info = info;
            _collection = collection;
            _enabler = new DefaultPartEnabler();
        }

        public IEnumerable<Mod> Mods
        {
            get { return _collection.Mods; }
        }

        public void EnableMod(Mod mod)
        {
            foreach (Part part in mod.Parts)
            {
                _enabler.Enable(_info, part);
            }
        }

        public void DisableMod(Mod mod)
        {
            foreach (Part part in mod.Parts)
            {
                _enabler.Disable(_info, part);
            }
        }
    }
}