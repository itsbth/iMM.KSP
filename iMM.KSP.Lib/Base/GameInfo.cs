namespace iMM.KSP.Lib.Base
{
    public class GameInfo
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