namespace iMM.KSP.Lib.Base
{
    public class PartInfo
    {
        public PartInfo(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Id { get; set; }
        public string Name { get; private set; }
    }
}