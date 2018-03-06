using System;
using System.Collections.Generic;
using System.Linq;

namespace Test3
{
    public class Validator
    {
        public static void CheckEquals(IEnumerable<Int64> a, IEnumerable<Int64> b)
        {
            if (a.Count(x => x != 0) != b.Count(x => x != 0))
            {
                Console.WriteLine($"Count failed - {a.Count(x => x != 0)} != {b.Count(x => x != 0)}");
            }
            foreach (var x in a)
            {
                if (x != 0 && b.Contains(x) == false)
                {
                    Console.WriteLine($"fail - {x} not found in b");
                    return;
                }
            }
            
            foreach (var x in b)
            {
                if (x != 0 && a.Contains(x) == false)
                {
                    Console.WriteLine($"fail - {x} not found in a");
                    return;
                }
            }
            
            Console.WriteLine("Check succeeded");
            
            
        }
    }
}