using System.Collections.Generic;
using iMM.KSP.Lib.Base;
using iMM.KSP.Lib.Enabler;
using iMM.KSP.Lib.Sources;

namespace iMM.KSP.Lib
{
    public class ModManager
    {
        private readonly BasePartEnabler _enabler;
        private readonly GameInfo _info;
        private readonly BaseModSource _source;

        public ModManager(GameInfo info, BaseModSource source)
        {
            _info = info;
            _source = source;
            _enabler = new DefaultPartEnabler();
        }

        public IEnumerable<Mod> Mods
        {
            get { return _source.Mods; }
        }

        public bool IsModEnabled(Mod mod)
        {
            return _info.EnabledMods.Contains(mod.Id);
        }

        public void EnableMod(Mod mod)
        {
            if (IsModEnabled(mod)) return;
            foreach (string file in mod.Files)
            {
                _enabler.Enable(_info, file);
            }
            _info.EnabledMods.Add(mod.Id);
        }

        public void DisableMod(Mod mod)
        {
            foreach (string file in mod.Files)
            {
                _enabler.Disable(_info, file);
            }
            _info.EnabledMods.Remove(mod.Id);
        }

        public void ToggleMod(Mod mod)
        {
            if (IsModEnabled(mod))
                DisableMod(mod);
            else
                EnableMod(mod);
        }
    }
}