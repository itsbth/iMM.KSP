namespace iMM.KSP.Lib.Base
{
    public class PartInfo
    {
        public enum Kind
        {
            Part,
            InternalSpace,
            InternalProp
        }

        public PartInfo(string id, string name, Kind type)
        {
            Id = id;
            Name = name;
            Type = type;
        }

        public string Id { get; set; }
        public string Name { get; private set; }
        public Kind Type { get; private set; }
    }
}