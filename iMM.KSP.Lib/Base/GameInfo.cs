using System.Collections.Generic;

namespace iMM.KSP.Lib.Base
{
    public class GameInfo
    {
        public GameInfo(string id, string name, string path) : this(id, name, path, new HashSet<string>())
        {
        }

        public GameInfo(string id, string name, string path, HashSet<string> enabledMods)
        {
            Id = id;
            Name = name;
            Path = path;
            EnabledMods = enabledMods;
        }

        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Path { get; private set; }
        public HashSet<string> EnabledMods { get; private set; }
    }
}