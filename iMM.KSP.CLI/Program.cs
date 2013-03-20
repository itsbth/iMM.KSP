using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Murmur;
using iMM.KSP.Lib;
using iMM.KSP.Lib.Base;
using iMM.KSP.Lib.Sources.ModFolder;
using iMM.KSP.Lib.Util;

namespace iMM.KSP.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            HashAlgorithm murmur128 = MurmurHash.Create128(managed: false); 
            var data = Encoding.ASCII.GetBytes("Hello, World!");
            var h2 = murmur128.ComputeHash(data);
            Console.WriteLine(String.Concat(h2.Select(b => String.Format("{0:x2}", b))));
            Console.ReadLine();
        }
    }
}
