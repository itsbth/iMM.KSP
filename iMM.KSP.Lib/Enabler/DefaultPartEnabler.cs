using System.IO;
using System.Linq;
using iMM.KSP.Lib.Base;

namespace iMM.KSP.Lib.Enabler
{
    public class DefaultPartEnabler : BasePartEnabler
    {
        public override bool Enable(GameInfo game, Part part)
        {
            string root = Path.Combine(game.Path, "Parts", part.Id);
            var files = part.Files.Select(f => new {Path = Path.Combine(root, f), Name = f}).ToList();
            if (files.Any(f => File.Exists(f.Path)))
                return false;
            if (!Directory.Exists(root))
                Directory.CreateDirectory(root);
            foreach (var file in files)
                using (Stream str = part.GetStream(file.Name), fs = File.OpenWrite(file.Path))
                    str.CopyTo(fs);
            return true;
        }

        public override bool Disable(GameInfo game, Part part)
        {
            string root = Path.Combine(game.Path, "Parts", part.Id);
            if (Directory.Exists(root))
                Directory.Delete(root, true);
            return true;
        }
    }
}