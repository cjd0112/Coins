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
            Console.WriteLine($"{n1.multiple},{n2.multiple},{n3.multiple},TotalCoins");
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
                    Console.WriteLine($"{j_coins},{k_coins},{l_coins},{g}");
                }
            }
            return cnt;
        }

        public static Int64 CalculateTotalCoinsForEachComboAndReturnCount(Int64[] arr, int valueForAssert, (int value, int multiple) n1, (int value, int multiple) n2, (int value, int multiple) n3, (int value, int multiple) n4)
        {
            Console.WriteLine($"{n1.multiple},{n2.multiple},{n3.multiple},{n4.multiple},TotalCoins");
            var transitionFactor1 = n1.multiple / n2.multiple;
            var transitionFactor2 = n2.multiple / n3.multiple;
            var transitionFactor3 = n3.multiple / n4.multiple;

            Int64 cnt = 0;

            for (var j = 0; j < n1.value; j++)
            {
                int j_coins = j + 1;


                var tf1 = transitionFactor1 * j;

                for (var k = 0; k < n2.value - tf1; k++)
                {
                    int k_coins = k + 1;

                    var tf2 = (tf1 * transitionFactor2) + (transitionFactor2 * k);

                    for (var l = 0; l < n3.value - tf2; l++)
                    {
                        int l_coins = l + 1;


                        var tf3 = (tf2 * transitionFactor3) + (transitionFactor3 * l);

                        int m_coins = n4.value - tf3;
#if DEBUG
                        if (arr.Length < valueForAssert)
                            throw new Exception("arr.length needs to be >= valueForAssert ");
                        
                        if (j_coins * n1.multiple + k_coins * n2.multiple + l_coins * n3.multiple +
                            m_coins * n4.multiple != valueForAssert)
                        {
                            throw new Exception($"found a combination that does not add up to our expected total - {valueForAssert}");
                        }
                        
#endif

                        var g = j_coins + k_coins + l_coins + m_coins;
                        arr[g]++;

                        Console.WriteLine($"{j_coins},{k_coins},{l_coins},{m_coins},{g}");

                    }
                }
            }
            return cnt;
        }


    }
}