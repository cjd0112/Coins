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
        
        public static Int64 CalculateTotalCoinsForEachComboAndReturnCount(Int64[] arr, int valueForAssert, (int value, int multiple) n1, (int value, int multiple) n2, (int value, int multiple) n3)
        {
            var transitionFactor1 = n1.multiple / n2.multiple;
            var transitionFactor2 = n2.multiple / n3.multiple;

            Int64 cnt = 0;

            for (var j = 0; j < n1.value; j++)
            {
                int j_coins = j + 1;

                var tf1 = transitionFactor1 * j;
                
                for (var k = 0; k < n2.value - tf1; k++)
                {
                    int k_coins = k + 1;

                    var tf2 = (tf1*transitionFactor2) + (transitionFactor2 * k);

                    int l_coins = n3.value - tf2;
                    var g = j_coins + k_coins + l_coins;
                    arr[g]++;
                    Console.WriteLine($"{j_coins}\t{k_coins}\t{l_coins}\t{g}");
                }
            }
            return cnt;
        }
        
    }
}