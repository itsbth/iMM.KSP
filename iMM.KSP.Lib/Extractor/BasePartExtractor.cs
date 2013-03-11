using iMM.KSP.Lib.Base;

namespace iMM.KSP.Lib.Extractor
{
    abstract class BasePartExtractor
    {
        public abstract bool Extract(GameInfo game, Part part);
    }

    internal class GameInfo
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Path { get; private set; }

        public GameInfo(string id, string name, string path)
        {
            Id = id;
            Name = name;
            Path = path;
        }
    }
}
