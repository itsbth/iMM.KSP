using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Caliburn.Micro;
using Microsoft.Win32;
using iMM.KSP.Lib;
using iMM.KSP.Lib.Base;
using iMM.KSP.Lib.Sources.ModFolder;

namespace iMM.KSP.UI.WPF
{
    internal class AppViewModel : Screen
    {
        private GameInfo _info;
        private ModManager _manager;
        private bool _notWorking;
        private IObservableCollection<ModInfo> _mods;
        private string _displayName;

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

        public IObservableCollection<ModInfo> Mods
        {
            get { return _mods; }
            private set
            {
                if (Equals(value, _mods)) return;
                _mods = value;
                NotifyOfPropertyChange(() => Mods);
            }
        }

        public void Loaded()
        {
            base.OnActivate();
            _info = File.Exists("config.json")
                        ? GameInfo.Load("config.json")
                        : new GameInfo("default", "Default", @"D:\Games\KSP\v.19-mod");
            var wm = new WindowManager();
            if (!_info.Data.ContainsKey("ModFolder"))
            {
                var model = new ConfigViewModel {Info = _info};
                if (!wm.ShowDialog(model).GetValueOrDefault())
                {
                    TryClose();
                }
            }
            var mcl = new ModFolderSource(_info.Data["ModFolder"]);
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

        public void Commit()
        {
            NotWorking = false;
            Task.Factory.StartNew(() =>
                {
                    foreach (ModInfo modInfo in Mods.Where(modInfo => modInfo.ShouldBeEnabled != modInfo.IsEnabled))
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

        public ModInfo(Mod mod, bool isEnabled)
        {
            Mod = mod;
            IsEnabled = isEnabled;
            ShouldBeEnabled = isEnabled;
        }

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

    internal class ConfigViewModel : Screen
    {
        public GameInfo Info { get; set; }

        public ConfigViewModel()
        {
            DisplayName = "iMM.KSP Config";
        }

        public string ModFolder
        {
            get { return Info.Data.ContainsKey("ModFolder") ? Info.Data["ModFolder"] : ""; }
            set { Info.Data["ModFolder"] = value; NotifyOfPropertyChange(() => ModFolder); }
        }

        public void OK()
        {
            TryClose(true);
        }

        public void SelectKSPFolder()
        {
            var dlg = new OpenFileDialog {Filter = "KSP Executable|KSP.exe", FileName = Info.Path};
            if (dlg.ShowDialog().GetValueOrDefault())
            {
                Info.Path = Path.GetDirectoryName(dlg.FileName);
            }
        }
    }
}