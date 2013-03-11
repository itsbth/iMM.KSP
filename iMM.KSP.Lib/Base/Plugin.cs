namespace iMM.KSP.Lib.Base
{
    public abstract class Plugin : PluginInfo
    {
        protected Plugin(string id, string name)
            : base(id, name)
        {
        }

        public abstract string Version { get; }
    }
}