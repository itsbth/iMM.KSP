namespace iMM.KSP.Lib.Base
{
    public class PluginInfo
    {
        public PluginInfo(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Id { get; private set; }
        public string Name { get; private set; }
    }
}