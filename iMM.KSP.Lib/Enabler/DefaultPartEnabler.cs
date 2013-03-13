using System.IO;
using System.Linq;
using iMM.KSP.Lib.Base;

namespace iMM.KSP.Lib.Enabler
{
    public class DefaultPartEnabler : BasePartEnabler
    {
        public override bool Enable(GameInfo game, string file1)
        {
            string type = file1.Type == PartInfo.Kind.Part
                              ? "Parts"
                              : file1.Type == PartInfo.Kind.InternalSpace ? "Internals/Spaces" : "Internals/Props";
            string root = Path.Combine(game.Path, type, file1.Id);
            var files = file1.Files.Select(f => new {Path = Path.Combine(root, f), Name = f}).ToList();

            //if (files.Any(f => File.Exists(f.Path)))
            //    return false;

            foreach (var folder in files.Select(fp => Path.GetDirectoryName(fp.Path)).Where(fp => !Directory.Exists(fp)))
                Directory.CreateDirectory(folder);

            foreach (var file in files)
                using (Stream str = file1.GetStream(file.Name), fs = File.OpenWrite(file.Path))
                    str.CopyTo(fs);
            return true;
        }

        public override bool Disable(GameInfo game, string file)
        {
            if (Directory.Exists(root))
                Directory.Delete(root, true);
            return true;
        }
    }
}