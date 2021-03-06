using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace iMM.KSP.Lib.Base
{
    public class GameInfo
    {

        public GameInfo() : this(null, null, null)
        {
        }

        public GameInfo(string id, string name, string path) : this(id, name, path, new HashSet<string>())
        {
        }

        public GameInfo(string id, string name, string path, HashSet<string> enabledMods)
        {
            Id = id;
            Name = name;
            Path = path;
            EnabledMods = enabledMods;
            Data = new Dictionary<string, string>();
            Files = new Dictionary<string, HashSet<string>>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public HashSet<string> EnabledMods { get; set; }
        public Dictionary<string, string> Data { get; set; }
        public Dictionary<string, HashSet<string>> Files;

        public static GameInfo Load(string configFile)
        {
            return JsonConvert.DeserializeObject<GameInfo>(File.ReadAllText(configFile));
        }

        public void Save(string configFile)
        {
            File.WriteAllText(configFile, JsonConvert.SerializeObject(this));
        }

        public string GetOwner(string path)
        {
            return Files.Where(mod => mod.Value.Contains(path)).Select(mod => mod.Key).FirstOrDefault();
        }
    }
}