using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iMM.KSP.Lib;
using iMM.KSP.Lib.Base;
using iMM.KSP.Lib.Enabler;
using iMM.KSP.Lib.Sources.ModFolder;

namespace iMM.KSP.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            var info = new GameInfo("default", "Default", @"D:\Games\KSP\v.dummy");
            var mcl = new ModFolderSource(@"D:\Games\KSP\Mods");
            var manager = new ModManager(info, mcl);
            Mod first = manager.Mods.First();
            manager.EnableMod(first);
            Console.WriteLine(manager.IsModEnabled(first));
            manager.DisableMod(first);
            Console.WriteLine(manager.IsModEnabled(first));
            Console.Read();
        }
    }
}
