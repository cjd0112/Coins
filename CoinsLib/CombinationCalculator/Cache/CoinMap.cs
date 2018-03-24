using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CoinsLib.CombinationCalculator.Cache
{
    public class CoinMap
    {
        public static int NumberDuplicates = 0;
        public static int NumberEntries = 0;
        private Dictionary<long, int> dict;
        public CoinMap()
        {
            dict = new Dictionary<long, int>((int)Math.Pow(2,24));
        }


        public void AddOrIncrementEntry(long key)
        {
            if (dict.TryGetValue(key, out var val))
            {
                NumberDuplicates++;
                val++;
                
            }
            else
            {
                val = 1;
                var arr = IntArrayToLong.ConvertFromLong(key);
                if (arr.Length == 7)
                    Console.WriteLine($"{arr[0]},{arr[1]},{arr[2]},{arr[3]},{arr[4]},{arr[5]},{arr[6]}");
                else if (arr.Length == 6)
                    Console.WriteLine($"0,{arr[0]},{arr[1]},{arr[2]},{arr[3]},{arr[4]},{arr[5]}");
                else if (arr.Length == 5)
                    Console.WriteLine($"0,0,{arr[0]},{arr[1]},{arr[2]},{arr[3]},{arr[4]}");
                else if (arr.Length == 4)
                    Console.WriteLine($"0,0,0,{arr[0]},{arr[1]},{arr[2]},{arr[3]}");
                else if (arr.Length == 3)
                    Console.WriteLine($"0,0,0,0,{arr[0]},{arr[1]},{arr[2]}");
                else if (arr.Length == 2)
                    Console.WriteLine($"0,0,0,0,0,{arr[0]},{arr[1]}");
                else if (arr.Length == 1)
                    Console.WriteLine($"0,0,0,0,0,0,{arr[0]}");
            }

            dict[key] = val;
            NumberEntries++;

        }


        public IEnumerable<(long coins, int numberTimes)> IterateValues()
        {
            foreach (var c in dict.Keys)
            {
                var val = dict[c];
                yield return (c, val);
            }
        }

     


    }
}
