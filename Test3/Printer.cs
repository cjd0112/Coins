using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using CoinsLib.Util;

namespace Test3
{
    public class Printer
    {
        public static void Print(IEnumerable<Int64> data, String msg)
        {
            Console.WriteLine(msg);
            data.Where(x=>x != 0).ForEach(x=>Console.WriteLine(x));
        }
    }
}