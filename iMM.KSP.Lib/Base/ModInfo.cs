namespace iMM.KSP.Lib.Base
{
    public class ModInfo
    {
        public ModInfo(string id, string name, string author, string version)
        {
            Id = id;
            Name = name;
            Author = author;
            Version = version;
        }

        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Author { get; private set; }
        public string Version { get; private set; }
    }
}