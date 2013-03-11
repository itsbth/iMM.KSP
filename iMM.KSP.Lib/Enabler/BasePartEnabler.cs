using iMM.KSP.Lib.Base;

namespace iMM.KSP.Lib.Enabler
{
    public abstract class BasePartEnabler
    {
        public abstract bool Enable(GameInfo game, Part part);
        public abstract bool Disable(GameInfo game, Part part);
    }
}