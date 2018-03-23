using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CoinsLib.CombinationCalculator.Cache
{
    public class CoinMap
    {
        private Dictionary<long, int> dict;
        public CoinMap()
        {
            dict = new Dictionary<long, int>((int)Math.Pow(2,24));
        }


        public void AddOrIncrementEntry(long key)
        {
            if (dict.TryGetValue(key ,out var val))
                val++;
            else
                val = 1;

            dict[key] = val;

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
