using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iMM.KSP.Lib.Base;
using iMM.KSP.Lib.Collection.ModFolder;
using iMM.KSP.Lib.Enabler;

namespace iMM.KSP.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            var mcl = new ModFolderModCollection(@"D:\Games\KSP\Mods");
            foreach (var mod in mcl.Mods)
            {
                Console.WriteLine("Mod: {0}", mod.Name);
                foreach (var part in mod.Parts)
                {
                    Console.WriteLine("\tPart: {0}", part.Name);
                }
            }
            var info = new GameInfo("default", "Default", @"D:\Games\KSP\v.dummy");
            var enabler = new DefaultPartEnabler();
            foreach (var part in mcl.Mods.First().Parts)
            {
                if (!enabler.Enable(info, part))
                    Console.WriteLine("Unable to enable {0}.", part.Id);
            }
            Console.Read();
        }
    }
}
