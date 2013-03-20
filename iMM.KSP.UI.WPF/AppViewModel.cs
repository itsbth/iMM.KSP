using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Caliburn.Micro;
using iMM.KSP.Lib;
using iMM.KSP.Lib.Base;
using iMM.KSP.Lib.Sources.ModFolder;

namespace iMM.KSP.UI.WPF
{
    internal class AppViewModel : Screen, IHaveDisplayName
    {
        private readonly ModManager _manager;
        private bool _notWorking;
        private GameInfo _info;

        public AppViewModel()
        {
            _info = File.Exists("config.json") ? GameInfo.Load("config.json") : new GameInfo("default", "Default", @"D:\Games\KSP\v.19-mod");
            var mcl = new ModFolderSource(@".\Mods");
            _manager = new ModManager(_info, mcl);
            Mods = new BindableCollection<ModInfo>(_manager.Mods.Select(CreateModInfo));
            NotWorking = true;
            DisplayName = "iMM KSP Mod Manager";
        }

        protected override void OnDeactivate(bool close)
        {
            base.OnDeactivate(close);
            if (close)
                _info.Save("config.json");
        }

        public bool NotWorking
        {
            get { return _notWorking; }
            set
            {
                if (value.Equals(_notWorking)) return;
                _notWorking = value;
                NotifyOfPropertyChange(() => NotWorking);
            }
        }

        public IObservableCollection<ModInfo> Mods { get; private set; }

        public string DisplayName { get; set; }

        public void Commit()
        {
            NotWorking = false;
            Task.Factory.StartNew(() =>
                {
                    foreach (var modInfo in Mods.Where(modInfo => modInfo.ShouldBeEnabled != modInfo.IsEnabled))
                    {
                        _manager.ToggleMod(modInfo.Mod);
                        modInfo.IsEnabled = modInfo.ShouldBeEnabled;
                    }
                    NotWorking = true;
                    _info.Save("config.json");
                });
        }

        private ModInfo CreateModInfo(Mod mod)
        {
            var info = new ModInfo(mod, _manager.IsModEnabled(mod));
            return info;
        }
    }

    internal class ModInfo : PropertyChangedBase
    {
        private bool _isEnabled;
        private Mod _mod;
        private bool _shouldBeEnabled;

        public bool ShouldBeEnabled
        {
            get { return _shouldBeEnabled; }
            set
            {
                if (value.Equals(_shouldBeEnabled)) return;
                _shouldBeEnabled = value;
                NotifyOfPropertyChange(() => ShouldBeEnabled);
            }
        }

        public ModInfo(Mod mod, bool isEnabled)
        {
            Mod = mod;
            IsEnabled = isEnabled;
            ShouldBeEnabled = isEnabled;
        }

        public Mod Mod
        {
            get { return _mod; }
            set
            {
                if (Equals(value, _mod)) return;
                _mod = value;
                NotifyOfPropertyChange(() => Mod);
            }
        }

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                if (value.Equals(_isEnabled)) return;
                _isEnabled = value;
                NotifyOfPropertyChange(() => IsEnabled);
            }
        }
    }
}